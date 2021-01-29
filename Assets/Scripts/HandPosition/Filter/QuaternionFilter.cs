using HandPosition.Input;
using UnityEngine;

namespace HandPosition.Filter
{
    public class QuaternionFilter : MonoBehaviour, IQuaternionFilter
    {
        // 调整率，Beta越大，惯性越大
        // [0, 1]
        private static float Beta = FilterConfiguration.GetBeta();
    
        // 角度和旋转轴从CVInput层获取
        private float _preAngle;
        private Vector3 _preAxis;
        private float _curAngle;
        private Vector3 _curAxis;
    
        // 处理过后的四元数交给显示层使用
        private Quaternion _predictQuaternion;
    
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
            UpdateFromPnP();
            LinearFilter();
            _preAngle = _curAngle;
            _preAxis = _curAxis;
        }

        private void UpdateFromPnP()
        {
            _curAngle = (float)_cvInput.OutAngle;
            _curAxis = _cvInput.OutAxis;
        }

        private void LinearFilter()
        {
            if (_preAngle == null || _preAxis == null)
            {
                _predictQuaternion = Quaternion.AngleAxis(_curAngle, _curAxis);
            }
            else
            {
                // 完成转型
                var predictAngle = Beta * _preAngle + (1 - Beta) * _curAngle;
                var predictAxis = Beta * _preAxis + (1 - Beta) * _curAxis;
                _predictQuaternion = Quaternion.AngleAxis(predictAngle,predictAxis);
                Debug.Log("Angles: "+predictAngle+" Axis: "+predictAxis);
                OutQuaternion = _predictQuaternion;
            }
        }
        
        public Quaternion OutQuaternion { get; private set; }
        public ulong OutTimeStamp { get; private set; }
    }
}
