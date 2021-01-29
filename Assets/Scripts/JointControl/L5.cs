using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class L5 : MonoBehaviour
{
    UduinoManager u;
    private int readValueTop;
    private int readValueMid;
    public GameObject L5top;
    public GameObject L5mid;
    // Start is called before the first frame update
    void Start()
    {
        u = UduinoManager.Instance;
        u.pinMode(AnalogPin.A6, PinMode.Input);
        u.pinMode(AnalogPin.A7,PinMode.Input);
    }

    // Update is called once per frame
    void Update()
    {
        SetAngles();
    }
    
    void SetAngles()
    {
        readValueTop = u.analogRead(AnalogPin.A6, "PinRead");
        readValueMid = u.analogRead(AnalogPin.A7, "PinRead");
        float topAngle = readValueTop * 90f / (1024f-700f);
        float midAngle = readValueMid * 90f / (1024f-700f);
        this.L5top.transform.localEulerAngles = new Vector3(-topAngle, 0, 0);
        this.L5mid.transform.localEulerAngles = new Vector3(-midAngle, 0, 0);
        UduinoManager.Instance.SendBundle("PinRead");
    }
}
