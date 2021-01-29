using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuaternionFilter 
{
    /// <summary>
    /// Dimension 4
    /// </summary>
    Quaternion OutQuaternion { get; }
    
    /// <summary>
    /// Time from start in ms
    /// </summary>
    ulong OutTimeStamp { get; }
}
