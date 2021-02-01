using System;
using Core.CoreLogic;
using UnityEngine;

namespace Core.Raw.RawOutput.ForceFeedback
{
    public class CollisionOutput : MonoBehaviour
    {
        //食指第2和第1关节是否发生碰撞
        private Boolean finger2MidCollision;
        private Boolean finger2TopCollision;

        //食指第2和第1关节的碰撞检测器
        private CollisionDetector finger2MidDetector;
        private CollisionDetector finger2TopDetector;
        
        // Start is called before the first frame update
        void Start()
        {
            finger2MidDetector = GameObject.Find("CH_Finger2_Base").GetComponent<CollisionDetector>();
            finger2TopDetector = GameObject.Find("CH_Finger2_Top").GetComponent<CollisionDetector>();
            finger2MidCollision = false;
            finger2TopCollision = false;
        }

        // Update is called once per frame
        void Update()
        {
            finger2MidCollision = finger2MidDetector.isCollided;
            finger2TopCollision = finger2TopDetector.isCollided;
        }
    }
}
