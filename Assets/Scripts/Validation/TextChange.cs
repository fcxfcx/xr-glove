using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour, IValidationMessage
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getMaxAngleInfo()
    {
        this.GetComponent<Text>().text = "请将手握至最紧";
    }

    public void getMinAngleInfo()
    {
        this.GetComponent<Text>().text = "干的漂亮，开始玩耍吧！";
    }
}
