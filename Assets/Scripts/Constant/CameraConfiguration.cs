namespace HandPosition
{
    public class CameraConfiguration
    {
        //畸变系数
        public static const double[] DIS_MATRIX = {1.92474559e-01, -5.23255110e-01,-1.63723773e-03,
            -1.63649544e-04, 6.01122558e-01  };

        //相机内参
        public static const double[,] CAMERA_MATRIX = {{6.86023804e+02, 0, 6.27946167e+02}, {0,
            6.87964478e+02,  3.57644836e+02 },{ 0, 0, 1} };

        //场景坐标系（yaml文件路径）
        public static const string MAP_PATH = "D://map.yml";
}
}
