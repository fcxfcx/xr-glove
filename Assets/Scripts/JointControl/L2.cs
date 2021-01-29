﻿using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class L2 : MonoBehaviour
{
    UduinoManager u;
    int readValueTop;
    int readValueMid;
    public GameObject L2top;
    public GameObject L2mid;
    // Start is called before the first frame update
    void Start()
    {
        u = UduinoManager.Instance;
        u.pinMode(AnalogPin.A0, PinMode.Input);
        u.pinMode(AnalogPin.A1, PinMode.Input);
    }

    // Update is called once per frame
    void Update()
    {
        SetAngles();
    }
    
    void SetAngles()
    {
        readValueTop = u.analogRead(AnalogPin.A0, "PinRead");
        readValueMid = u.analogRead(AnalogPin.A1, "PinRead");
        float angleTop = readValueTop * 90f / 360f;
        float angleMid = readValueMid * 90f / 300f;
        this.L2top.transform.localEulerAngles = new Vector3(-angleTop, 0, 0);
        this.L2mid.transform.localEulerAngles = new Vector3(-angleMid, 0, 0);
        UduinoManager.Instance.SendBundle("PinRead");
    }
}