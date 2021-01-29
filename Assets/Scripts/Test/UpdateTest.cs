using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTest : MonoBehaviour
{
    private static int D = 1024 * 8;
    private static int[,] someMatrix = new int[D,D];
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start test");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Update");
        for (int i = 0; i < D; i++)
        {
            for (int j = 0; j < D; j++)
            {
                someMatrix[j,i] = 10;
            }
        }
    }
}
