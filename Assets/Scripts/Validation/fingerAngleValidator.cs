using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class fingerAngleValidator : MonoBehaviour
{
    UduinoManager u;
    public float minAngle;
    public float maxAngle;
    private List<float> min;
    private List<float> max;
    int readValue;
    // Start is called before the first frame update
    void Start()
    {
        u = UduinoManager.Instance;
        u.pinMode(AnalogPin.A0, PinMode.Input);
    }

    // Update is called once per frame
    void Update()
    {
        if(min.Count <= 100)
        {
            getMin();
        }
    }

    void getMin()
    {
        readValue = u.analogRead(AnalogPin.A0, "PinRead");
        float angle = (readValue * 200f) / 1024f;
        Debug.Log(angle);
        min.Add(angle);
    }

    void getMax()
    {
        readValue = u.analogRead(AnalogPin.A0, "PinRead");
        float angle = (readValue * 200f) / 1024f;
        max.Add(angle);
    }
}
