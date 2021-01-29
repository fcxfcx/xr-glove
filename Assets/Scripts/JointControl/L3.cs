﻿using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class L3 : MonoBehaviour
{
    UduinoManager u;
    int readValueTop;
    int readValueMid;
    public GameObject L3top;
    public GameObject L3mid;
    // Start is called before the first frame update
    void Start()
    {
        u = UduinoManager.Instance;
        u.pinMode(AnalogPin.A2, PinMode.Input);
        u.pinMode(AnalogPin.A3, PinMode.Input);
    }

    // Update is called once per frame
    void Update()
    {
        SetAngles();
    }
    
    void SetAngles()
    {
        readValueTop = u.analogRead(AnalogPin.A2, "PinRead");
        readValueMid = u.analogRead(AnalogPin.A3, "PinRead");
        float angleTop = readValueTop * 90f / 300f;
        float angleMid = readValueMid * 90f / 250f;
        this.L3top.transform.localEulerAngles = new Vector3(-angleTop, 0, 0);
        this.L3mid.transform.localEulerAngles = new Vector3(-angleMid, 0, 0);
        UduinoManager.Instance.SendBundle("PinRead");
    }
}
