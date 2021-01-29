using System.Collections.Generic;
using UnityEngine;
using Uduino;
using System;
using System.Threading;
using System.IO.Ports;

public class R4 : MonoBehaviour
{
    UduinoManager u;
    int readValue;
    public GameObject R4top;
    public GameObject R4mid;

    // Start is called before the first frame update
    void Start()
    {
        u = UduinoManager.Instance;
        u.pinMode(AnalogPin.A0, PinMode.Input);
    }

    // Update is called once per frame
    void Update()
    {
        SetAngles();
        //transform.localEulerAngles = new Vector3(angles[0], 0, 0);
        //R4mid.transform.localEulerAngles = new Vector3(angles[1], 0, 0);
        //R4top.transform.localEulerAngles = new Vector3(angles[2], 0, 0);
    }

    void SetAngles()
    {
        readValue = u.analogRead(AnalogPin.A0, "PinRead");
        float angle = (readValue * 200f) / 1024f;
        this.R4mid.transform.localEulerAngles = new Vector3(angle, 0, 0);
        UduinoManager.Instance.SendBundle("PinRead");
    }

}
