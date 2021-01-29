using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenCvSharp;
using OpenCvSharp.Aruco;
using UnityEngine;

namespace HandPosition.Input
{
    public class CvInput : MonoBehaviour, ICvInput
    {
        private VideoCapture _capture;
        private Mat _frame;
    
        private readonly Dictionary _arDict = CvAruco.GetPredefinedDictionary(PredefinedDictionaryName.DictArucoOriginal);
        private readonly DetectorParameters _parameters = DetectorParameters.Create();
        private Dictionary<int, Point3f[]> _markerWorldPointsDictionary;
    
        private Stopwatch _stopwatch = new Stopwatch();
        private long _duration = 0;
        private long _count = 0;
    
        public double ValidFps
        {
            get
            {
                return _count * 1000 / _duration;
            }
        }
        public long timeForDetection = 0;
        public long timeForPnP = 0;
        private int _detectedMarkersNum = 0;

        private static readonly double[] DisArray = CameraConfiguration.GetDisArray();
        private static readonly double[,] CameraArray = CameraConfiguration.GetCameraArray();

        private static InputArray _distortionCoefficients;

        private static InputArray _cameraMatrix;

        private static readonly String YamlPath = CameraConfiguration.GetYamlPath();
        // Start is called before the first frame update
        void Start()
        {
            _capture = new VideoCapture(0);
            _distortionCoefficients = InputArray.Create(DisArray);
            _cameraMatrix = InputArray.Create(CameraArray);
            _markerWorldPointsDictionary = Yaml2MarkersMap.ReadAndParse(YamlPath);
            _frame = new Mat();
        }

        // Update is called once per frame
        void Update()
        {
            // _stopwatch.Restart();
            _capture.Read(_frame);

            _detectedMarkersNum = 0;
        
            CvAruco.DetectMarkers(_frame, _arDict, out var markerCorners,
                out var markersIds, _parameters, out _);
            // timeForDetection = _stopwatch.ElapsedMilliseconds;
            // _duration += timeForDetection;

            if (markersIds?.Length != 0)
            {
                _detectedMarkersNum = markersIds.Length;
            
                //PnPSolving Returns
                var rVec = OutputArray.Create(new Mat());
                var tVec = OutputArray.Create(new Mat());
            
                //2d-pos & world 3d-pos
                IList<Point2f> imagePoints = new List<Point2f>();
                IList<Point3f> worldPoints = new List<Point3f>();

                for (var i = 0; i < _detectedMarkersNum; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        imagePoints.Add(markerCorners[i][j]);
                        worldPoints.Add(_markerWorldPointsDictionary[markersIds[i]][j]);
                    }
                }

                try
                {
                    //PnP算法求得旋转向量和位移向量
                    Cv2.SolvePnP(InputArray.Create(worldPoints), InputArray.Create(imagePoints),
                        _cameraMatrix, _distortionCoefficients, rVec, tVec);
                
                    //解旋转向量到旋转矩阵（罗德里格斯变化）
                    InputArray inputRvec = InputArray.Create(rVec.GetMat());
                    OutputArray outputR = OutputArray.Create(new Mat());
                    Cv2.Rodrigues(inputRvec,outputR);
                    var tempRMatrix = TranslateMatrix(outputR.GetMat());
                
                    //由旋转矩阵求得旋转轴和旋转角
                    OutAngle = GetOutputAngle(tempRMatrix);
                    OutAxis = GetOutputAxis(tempRMatrix, OutAngle);
                
                    //由位移向量转换为Unity支持形式
                    OutTVec = ToTransformVector3(tVec);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    

        /// <summary>
        /// 将旋转矩阵有Mat类型转换成二维double数组
        /// </summary>
        /// <param name="rotationMatrix"></param>
        /// <returns>double数组</returns>
        private double[][] TranslateMatrix(Mat rotationMatrix)
        {
            double[][] doubleMatrix = new double[3][];
            for (int index = 0; index < 3; index++)
            {
                doubleMatrix[index] = new double[3];
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    doubleMatrix[i][j] = rotationMatrix.Get<double>(i, j);
                }
            }

            return doubleMatrix;
        }
    
        /// <summary>
        /// 从旋转矩阵求得旋转角
        /// </summary>
        /// <param name="doubleMatrix"></param>
        /// <returns>旋转角</returns>
        private double GetOutputAngle(double[][] doubleMatrix)
        {
            //由旋转矩阵求解旋转角的公式
            double retAngle = Math.Acos((doubleMatrix[0][0] + doubleMatrix[1][1] + doubleMatrix[2][2] - 1) / 2) / Math.PI *
                              180;
            return retAngle;
        }

        /// <summary>
        /// 从旋转矩阵求得旋转轴
        /// </summary>
        /// <param name="doubleMatrix"></param>
        /// <param name="angle"></param>
        /// <returns>旋转轴向量</returns>
        private Vector3 GetOutputAxis(double[][] doubleMatrix,double angle)
        {
            var ret = new Vector3();
            //常数系数
            var alpha = 0.5 * Math.Cos(Math.PI / (180 / angle));
            ret.x = (float) ((doubleMatrix[2][1] - doubleMatrix[1][2]) * alpha);
            ret.y = (float) ((doubleMatrix[0][2] - doubleMatrix[2][0]) * alpha);
            ret.z = (float) ((doubleMatrix[1][0] - doubleMatrix[0][1]) * alpha);
            return ret;
        }

        /// <summary>
        /// 将位移向量转换为Unity支持形式
        /// </summary>
        /// <param name="tvec"></param>
        /// <returns></returns>
        private Vector3 ToTransformVector3(OutputArray tvec)
        {
            double[] temp; 
            Mat cvMat = tvec.GetMat();
            cvMat.GetArray<double>(out temp);
            var ret = new Vector3((float)temp[0],(float)temp[1],(float)temp[2]);
            return ret;
        }

        public Vector3 OutTVec { get; private set; }
        public double OutAngle { get; private set; }
        public Vector3 OutAxis { get; private set; }
        public ulong OutTimeStamp { get; private set; }
    }
}
