using UnityEngine;
using System;
using HandPosition.Input;

namespace HandPosition.Filter
{
    public class TransformFilter : MonoBehaviour, ITransformFilter
    {
        //从CVInput层获取的位移向量
        private Vector3 _preVector;
        private Vector3 _curVector;
        
        //处理后交给显示层使用的位移向量
        private Vector3 _predictVector;

        //计算用数
        private static double Q = FilterConfiguration.KALMAN_Q;
        private static double R = FilterConfiguration.KALMAN_R;
        private double _pDelt = FilterConfiguration.KALMAN_P_DELT;
        private double _mDelt = FilterConfiguration.KALMAN_M_DELT;
        private double _gauss;
        private double _kalmanGain;
        
        //摄像头数据
        private CvInput _cvInput; 
        
        // Start is called before the first frame update
        void Start()
        {
            _cvInput = GameObject.Find("CVInput").GetComponent<CvInput>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            UpdateFromPnp();
            KalmanFilter();
            _preVector = _curVector;
        }
        
        private void UpdateFromPnp()
        {
            // 从PnP算法层拿
            _curVector = _cvInput.OutTVec;
        }
        
        private void KalmanFilter()
        {
            if (_preVector == null)
            {
                _predictVector = _curVector;
            }
            else
            {
                _gauss = Math.Sqrt(_pDelt * _pDelt + _mDelt * _mDelt) + Q; 
                _kalmanGain = Math.Sqrt((_gauss * _gauss) / (_gauss * _gauss + _pDelt * _pDelt)) + R;
                _predictVector = ((float)_kalmanGain * (_curVector - _preVector)) + _preVector;
                _mDelt = Math.Sqrt((1 - _kalmanGain) * _gauss * _gauss);
                OutTVec = _predictVector;
            }
        }
        
        public Vector3 OutTVec { get; private set; }
        public ulong OutTimeStamp { get; private set; }
    }
}
