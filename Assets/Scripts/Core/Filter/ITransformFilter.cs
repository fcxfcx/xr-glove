using UnityEngine;

namespace HandPosition.Filter
{
    public interface ITransformFilter
    {
        /// <summary>
        /// 输出给显示层的位移vector
        /// </summary>
        Vector3 OutTVec { get; }
        
        ulong OutTimeStamp { get; }
    }
}
