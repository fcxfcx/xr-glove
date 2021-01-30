using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    Vector3 oldMousePosition;
    Vector3 newMousePosition;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        oldMousePosition = Input.mousePosition;
        newMousePosition = Input.mousePosition;
        target = GameObject.Find("Text");
    }
    
    void FixedUpdate()
    {
        newMousePosition = Input.mousePosition;
        if (newMousePosition != oldMousePosition)
        {
            transform.Rotate(Vector3.up, newMousePosition.x - oldMousePosition.x);
            transform.Rotate(Vector3.right, -1 * (newMousePosition.y - oldMousePosition.y));
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
        oldMousePosition = Input.mousePosition;
    }
}
