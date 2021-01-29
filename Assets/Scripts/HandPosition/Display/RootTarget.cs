using System.Collections.Generic;
using HandPosition.Filter;
using Interface;
using OpenCvSharp;
using UnityEngine;

namespace TargetHand
{
    public class RootTarget : MonoBehaviour
    {
        private Transform _transform;

        private QuaternionFilter _quaternionFilter;
        private TransformFilter _transformFilter;

        // Start is called before the first frame update
        void Start()
        {
            _quaternionFilter = GameObject.Find("QuaternionFilter").GetComponent<QuaternionFilter>();
            _transformFilter = GameObject.Find("TransformFilter").GetComponent<TransformFilter>();
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _transform.position = _transformFilter.OutTVec;
            _transform.rotation = _quaternionFilter.OutQuaternion;
        }
    }
}
