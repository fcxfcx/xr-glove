using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootMove : MonoBehaviour
{
    private Transform transform;
    private Transform targetTransform;
    public GameObject targetObject;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();
        targetTransform = targetObject.GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        transform.rotation = targetTransform.rotation;
    }
}
