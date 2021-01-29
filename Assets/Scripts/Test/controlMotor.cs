using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class controlMotor : MonoBehaviour
{
    [Range(-255,255)]
    public int motorAngle;
    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.pinMode(10, PinMode.Output);
        UduinoManager.Instance.pinMode(11, PinMode.Output);
    }

    // Update is called once per frame
    void Update()
    {
        if(motorAngle >= 0)
        {
            UduinoManager.Instance.analogWrite(10, motorAngle);
            UduinoManager.Instance.digitalWrite(11, State.LOW);
        }
        else
        {
            UduinoManager.Instance.analogWrite(11, -motorAngle);
            UduinoManager.Instance.digitalWrite(10, State.LOW);
        }
    }
}
