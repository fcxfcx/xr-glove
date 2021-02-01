using Core.CoreLogic;
using UnityEngine;

namespace Core.Raw.RawOutput.ForceFeedback
{
    public class DistanceOutput : MonoBehaviour
    {
        //食指第2和第1关节力反馈距离
        private float finger2TopDistance;
        private float finger2MidDistance;
    
        //用于检测距离的集成类
        private ForceCalculator forceCalculator;
    
        // Start is called before the first frame update
        void Start()
        {
            forceCalculator = new ForceCalculator();
        }

        // Update is called once per frame
        void Update()
        {
            //2代表食指的序号，目前仅作为实验用
            finger2TopDistance = forceCalculator.GetFingerTopDistance(2);
            finger2MidDistance = forceCalculator.GetFingerMidDistance(2);
        }
    }
}
