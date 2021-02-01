using System.Text;
using UnityEngine;

namespace Core.CoreLogic
{
    /// <summary>
    ///该类用于可见手的各个关节向碰撞手的特定关节移动
    /// </summary>
    public class TargetMove : MonoBehaviour
    {
        private GameObject targetObject;
        // Start is called before the first frame update
        void Start()
        {
            targetObject = GameObject.Find(GetTargetName());
        }

        // Update is called once per frame
        void Update()
        {
            var transform1 = transform;
            transform1.position = targetObject.transform.position;
            transform1.rotation = targetObject.transform.rotation;
        }

        /// <summary>
        /// 获取当前物体的目标物体名
        /// </summary>
        /// <returns></returns>
        private string GetTargetName()
        {
            string thisName = this.name;
            string[] temp = thisName.Split('_');
            temp[0] = "CH";
            StringBuilder targetName = new StringBuilder();
            for (var i = 0; i < temp.Length; i++)
            {
                targetName.Append(temp[i]);
                if (i != temp.Length - 1)
                {
                    targetName.Append("_");
                }
            }

            return targetName.ToString();
        }
    }
}
