using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using Interface;
using UnityEngine.Serialization;

public class Detector : MonoBehaviour
{
    private static string yamlPath = "D://map.yml";
    private readonly Dictionary<int, Point3f[]> _markersDictionary = Yaml2MarkersMap.ReadAndParse(yamlPath);
    private CameraStream2 _cameraStream;

    private static readonly double[] DisArray = {1.92474559e-01, -5.23255110e-01,-1.63723773e-03,
        -1.63649544e-04, 6.01122558e-01  };
    private static readonly double[,] CameraArray = {{6.86023804e+02, 0, 6.27946167e+02}, {0,
        6.87964478e+02,  3.57644836e+02 },{ 0, 0, 1} };
    private static readonly InputArray DistortionCoefficients = InputArray.Create(DisArray);
    private static readonly InputArray CameraMatrix = InputArray.Create(CameraArray);

    private Transform _transform;
    public Vector3 Position;
    public Vector3 EulerAngle;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        VideoCapture capture = new VideoCapture(0);
        _cameraStream = new CameraStream2(capture, DistortionCoefficients, CameraMatrix, _markersDictionary);
        _cameraStream.Start();
    }

    // Update is called once per frame
    void Update()
    {
        _cameraStream.Update();
        Debug.Log("Valid FPS: "+_cameraStream.ValidFps + " TimePerCalculation: "+_cameraStream.TimePerCalculation+" TimeForDetection: "+_cameraStream.timeForDetection);
        if (_cameraStream.Position != null && _cameraStream.EulerAngles != null)
        {
            Position = new Vector3(-_cameraStream.Position[0], -_cameraStream.Position[1], _cameraStream.Position[2]);
            EulerAngle = new Vector3(-(float)_cameraStream.EulerAngles[0],-(float)(_cameraStream.EulerAngles[1]),(float)(_cameraStream.EulerAngles[2]));
        }
    }
}
