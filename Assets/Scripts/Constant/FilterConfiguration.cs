namespace HandPosition
{
    public class FilterConfiguration
    {
        //注释待完善
        public const float KALMAN_Q = 0.00001f;
        public const float KALMAN_R = 0.1f;
        public const float KALMAN_P_DELT = 4.0f;
        public const float KALMAN_M_DELT = 3.0f;
        
        // 调整率，Beta越大，惯性越大
        // [0, 1]
        public const float LINEAR_BETA = 0.5f;
    }
}
