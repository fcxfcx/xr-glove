using UnityEngine;

public interface ICvInput
{
    /// <summary>
    /// 输出给滤波曾的位移vector
    /// </summary>
    Vector3 OutTVec { get; }
    
    /// <summary>
    /// 输出给滤波层的旋转角度
    /// </summary>
    double OutAngle { get; }
    
    /// <summary>
    /// 输出给滤波曾的旋转轴
    /// </summary>
    Vector3 OutAxis { get; }

    /// <summary>
    /// Time from start in ms
    /// </summary>
    ulong OutTimeStamp { get; }
}
