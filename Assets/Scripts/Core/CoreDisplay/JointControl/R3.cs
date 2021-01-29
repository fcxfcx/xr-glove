using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R3 : MonoBehaviour
{
    public GameObject R3top;
    public GameObject R3mid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(-0.25f, 0, 0, Space.Self);
            R3mid.transform.Rotate(-1, 0, 0, Space.Self);
            R3top.transform.Rotate(-0.5f, 0, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.25f, 0, 0, Space.Self);
            R3mid.transform.Rotate(1, 0, 0, Space.Self);
            R3top.transform.Rotate(0.5f, 0, 0, Space.Self);
        }
    }
}
