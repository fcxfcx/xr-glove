using System;
using UnityEngine;

namespace Core.CoreLogic
{
    /// <summary>
    /// 该类用于检测各关节的碰撞情况，需要挂载到各个碰撞体上生效
    /// </summary>
    public class CollisionDetector : MonoBehaviour
    {
        public Boolean isCollided;

        public void Start()
        {
            isCollided = false;
        }

        public void OnCollisionEnter(Collision other)
        {
            isCollided = true;
        }

        public void OnCollisionExit(Collision other)
        {
            isCollided = false;
        }
    }
}
