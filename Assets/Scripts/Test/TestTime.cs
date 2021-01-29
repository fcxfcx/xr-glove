﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenCvSharp;
using OpenCvSharp.Aruco;

namespace Test
{
    public class TestTime
    {
        private static string yamlPath = "D://map.yml";
        private static readonly double[] DisArray = {-1.74318813e-02, -1.42444298e-02, 1.11860491e-03,
            5.83569566e-03, 1.23203956e-02 };
        private static readonly double[,] CameraArray = {{9.94741333e+02, 0, 6.31531494e+02}, {0,
            9.95352051e+02,  4.84937897e+02 },{ 0, 0, 1} };
        private static readonly InputArray DistortionCoefficients = InputArray.Create(DisArray);
        private static readonly InputArray CameraMatrix = InputArray.Create(CameraArray);

        public static void main()
        {
            VideoCapture capture = new VideoCapture(0);
            Dictionary<int, Point3f[]> _markersDictionary = Yaml2MarkersMap.ReadAndParse(yamlPath);
            CameraStream3 _cameraStream = new CameraStream3(capture, DistortionCoefficients, CameraMatrix, _markersDictionary);
            _cameraStream.Start();
            for (int i = 0; i < 1000; i++)
            {
                _cameraStream.Update();
                Console.WriteLine("Valid FPS: "+_cameraStream.ValidFps + " TimePerCalculation: "+_cameraStream.TimePerCalculation+" Detected Markers: "+_cameraStream.DetectedMarkers);
            }
        }
    }
    
    public class CameraStream3
    {
        public VideoCapture Capture { get; private set; }
        public InputArray DistortionCoefficients { get; private set; }
        public InputArray CameraMatrix { get; private set; }
        public Mat Frame { get; private set; }

        public double ValidFps
        {
            get
            {
                return count * 1000 / duration;
            }
        }

        public double TimePerCalculation = 0;
        public int DetectedMarkers = 0;

        private readonly Dictionary arDict = CvAruco.GetPredefinedDictionary(PredefinedDictionaryName.DictArucoOriginal);
        private readonly DetectorParameters parameters = DetectorParameters.Create();
        private Dictionary<int, Point3f[]> markerWorldPointsDictionary;

        private int garbageCount = 0;
        private const uint gcGeneration = 10;
        private Stopwatch _stopwatch = new Stopwatch();
        private long duration = 0;
        private long count = 0;

        /// <summary>
        /// 上一个Update后的世界系位置坐标。建议进行try-catch surrounding或者为空排查。
        /// </summary>
        public float[] Position { get; private set; }

        /// <summary>
        /// 上一个Update后的欧拉角。建议进行try-catch surrounding或者为空排查。
        /// </summary>
        public double[] EulerAngles { get; private set; }

        /// <summary>
        /// 创建一个摄像头输入流
        /// </summary>
        /// <param name="capture">输入设备</param>
        /// <param name="distortionCoefficients">畸变矩阵</param>
        /// <param name="cameraMatrix">内参矩阵</param>
        /// <param name="markerDictionary">marker的世界系三维坐标</param>
        public CameraStream3(OpenCvSharp.VideoCapture capture, InputArray distortionCoefficients,
            InputArray cameraMatrix, Dictionary<int, Point3f[]> markerDictionary)
        {
            Capture = capture;
            DistortionCoefficients = distortionCoefficients;
            CameraMatrix = cameraMatrix;
            markerWorldPointsDictionary = markerDictionary;
        }

        /// <summary>
        /// 开启流
        /// </summary>
        public void Start()
        {
            Frame = new Mat(@"D:/TEST.jpg");
            _stopwatch.Start();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            _stopwatch.Restart();
            DetectedMarkers = 0;

            CvAruco.DetectMarkers(Frame, arDict, out Point2f[][] markerCorners,
                out int[] markersIDs, parameters, out _);
            //进行markers检测，返回角点2D坐标集

            if(markersIDs?.Length != 0)
            {
                DetectedMarkers = markersIDs.Length;
                OutputArray rvec = OutputArray.Create(new Mat());
                OutputArray tvec = OutputArray.Create(new Mat());
                //返回PnPSolving的平移向量和旋转向量

                IList<Point2f> imagePoints = new List<Point2f>();
                IList<Point3f> worldPoints = new List<Point3f>();
                //对应的2D图像坐标和3D世界系坐标的点击，一一对应

                for (int i = 0; i < markersIDs.Length; i++)
                {
                    for(int j = 0; j < 4; j++)
                    {
                        imagePoints.Add(markerCorners[i][j]);
                        worldPoints.Add(markerWorldPointsDictionary[markersIDs[i]][j]);
                    }
                    break;
                }
                //将图像点坐标和世界系点坐标对应压入容器

                try
                {
                    Cv2.SolvePnP(InputArray.Create(worldPoints), InputArray.Create(imagePoints),
                        CameraMatrix, DistortionCoefficients, rvec, tvec);
                    //利用高鲁棒算法求解PnP问题

                    InputArray inputRvec = InputArray.Create(rvec.GetMat());
                    OutputArray outputR = OutputArray.Create(new Mat());
                    Cv2.Rodrigues(inputRvec,outputR);

                    var tempPosition = tvec.GetMat().Get<Vec3d>();
                    var tempRMatrix = outputR.GetMat();
                    //解rvec到旋转矩阵

                    Position = new float[] { (float)tempPosition[0], (float)tempPosition[1], (float)tempPosition[2] };
                    EulerAngles = RotationMatrixToEulerAngles(tempRMatrix);

                    count++;
                }
                catch (Exception e)
                {
                    string m = e.Message;
                }

                Collect();
                _stopwatch.Stop();
                TimePerCalculation = _stopwatch.ElapsedMilliseconds;
                duration += _stopwatch.ElapsedMilliseconds;
            }


        }

        private double[] RotationMatrixToEulerAngles(Mat rotationMatrix)
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

            double sy = Math.Sqrt(doubleMatrix[0][0] * doubleMatrix[0][0] +
                doubleMatrix[1][0] * doubleMatrix[1][0]);
            bool singular = sy < 1e-6;

            double x, y, z;

            if (!singular)
            {
                x = Math.Atan2(doubleMatrix[2][1], doubleMatrix[2][2]);
                y = Math.Atan2(-doubleMatrix[2][0], sy);
                z = Math.Atan2(doubleMatrix[1][0], doubleMatrix[0][0]);
            }
            else
            {
                x = Math.Atan2(-doubleMatrix[1][2], doubleMatrix[1][1]);
                y = Math.Atan2(-doubleMatrix[2][0], sy);
                z = 0;
            }

            x = x * 180.0 / Math.PI;
            y = y * 180.0 / Math.PI;
            z = z * 180.0 / Math.PI;

            return new double[3] { x, y, z };
        }
        private void Collect()
        {
            if(garbageCount > gcGeneration)
            {
                GC.Collect();
                garbageCount = 0;
            }

            garbageCount++;
        }
    }
}
