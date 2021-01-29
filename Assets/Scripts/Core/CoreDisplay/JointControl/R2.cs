using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class R2 : MonoBehaviour
{
    public GameObject R2top;
    public GameObject R2mid;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-0.25f,0,0,Space.Self);
            R2mid.transform.Rotate(-1,0,0,Space.Self);
            R2top.transform.Rotate(-0.5f,0,0,Space.Self);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(0.25f, 0, 0, Space.Self);
            R2mid.transform.Rotate(1, 0, 0, Space.Self);
            R2top.transform.Rotate(0.5f, 0, 0, Space.Self);
        }
    }
}
