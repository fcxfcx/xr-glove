using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class L4 : MonoBehaviour
{
    UduinoManager u;
    int readValueTop;
    int readValueMid;
    public GameObject L4top;
    public GameObject L4mid;
    // Start is called before the first frame update
    void Start()
    {
        u = UduinoManager.Instance;
        u.pinMode(AnalogPin.A4, PinMode.Input);
        u.pinMode(AnalogPin.A5, PinMode.Input);
    }

    // Update is called once per frame
    void Update()
    {
        SetAngles();
    }
    
    void SetAngles()
    {
        readValueTop = u.analogRead(AnalogPin.A4, "PinRead");
        readValueMid = u.analogRead(AnalogPin.A5, "PinRead");
        float angleTop = readValueTop * 90f / 360f;
        float angleMid = readValueMid * 90f / 360f;
        this.L4top.transform.localEulerAngles = new Vector3(-angleTop, 0, 0);
        this.L4mid.transform.localEulerAngles = new Vector3(-angleMid, 0, 0);
        UduinoManager.Instance.SendBundle("PinRead");
    }
}
