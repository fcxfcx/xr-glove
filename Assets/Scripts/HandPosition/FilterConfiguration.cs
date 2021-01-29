namespace HandPosition
{
    public class FilterConfiguration
    {
        //注释待完善
        private static double Q = 0.00001;
        private static double R = 0.1;
        private static double _pDelt = 4.0;
        private static double _mDelt = 3.0;
        
        // 调整率，Beta越大，惯性越大
        // [0, 1]
        private static float _beta = 0.5f;
        
        public static double GetQ() => Q;

        public static double GetR() => R;

        public static double GetPDelt() => _pDelt;

        public static double GetMDelt() => _mDelt;

        public static float GetBeta() => _beta;
    }
}
