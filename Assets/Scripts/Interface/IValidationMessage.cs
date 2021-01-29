using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public interface IValidationMessage : IEventSystemHandler
{
    void getMinAngleInfo();
    void getMaxAngleInfo();
}
