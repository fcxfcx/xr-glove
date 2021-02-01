using System.Text;
using UnityEngine;

namespace Core.CoreLogic
{
    /// <summary>
    /// 该类用于探测不同手指关节的受力，输出力反馈数值
    /// </summary>
    public class ForceCalculator
    {
        //可见手和碰撞手的关节GameObject
        private GameObject collisionFingerMid;
        private GameObject collisionFingerTop;
        private GameObject visualFingerMid;
        private GameObject visualFingerTop;


        /// <summary>
        /// 获取食指第二关节受力距离（对应根部齿轮）
        /// </summary>
        /// <param name="index">手指的序号，从1至5对应大拇指到小指</param>
        /// <returns></returns>
        public float GetFingerMidDistance(int index)
        {
            //索引碰撞手所用的物体名
            string collisionName = "CH_Finger" + index + "_Base";
            //索引可见手所用的物体名
            string visualName = "VH_Finger" + index + "_Base";
            
            collisionFingerMid = GameObject.Find(collisionName);
            visualFingerMid = GameObject.Find(visualName);

            float distance =
                Vector3.Distance(collisionFingerMid.transform.position, visualFingerMid.transform.position);
            return distance;
        }
        
        /// <summary>
        /// 食指第一关节受力距离（对应靠近手指尖齿轮）
        /// </summary>
        /// <param name="index">手指的序号，从1至5对应大拇指到小指</param>
        /// <returns></returns>
        public float GetFingerTopDistance(int index)
        {
            //索引碰撞手所用的物体名
            string collisionName = "CH_Finger" + index + "_Top";
            //索引可见手所用的物体名
            string visualName = "VH_Finger" + index + "_Top";
            
            collisionFingerTop = GameObject.Find(collisionName);
            visualFingerTop = GameObject.Find(visualName);

            float distance =
                Vector3.Distance(collisionFingerTop.transform.position, visualFingerTop.transform.position);
            return distance;
        }
    }
}
