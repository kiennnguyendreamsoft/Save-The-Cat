using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSizeScreen : MonoBehaviour
{
    public Transform blockTop, blockLeft, blockRight, blockBottom;
    // Start is called before the first frame update
    void Start()
    {
        SetupPosition();
    }

    public void SetupPosition()
    {
        float resolution = (float) Screen.height / Screen.width;
        if (resolution > 2.1f)
        {
            blockLeft.position = new Vector3(-8.6f, 0, 0);
            blockRight.position = new Vector3(8.6f, 0, 0);
        }
        else if (resolution > 2f)
        {
            blockLeft.position = new Vector3(-8.9f, 0, 0);
            blockRight.position = new Vector3(8.9f, 0, 0);
        }
        else if (resolution > 1.8f)
        {
            blockLeft.position = new Vector3(-9f, 0, 0);
            blockRight.position = new Vector3(9f, 0, 0);
        }
        else if (resolution > 1.6f)
        {
            blockLeft.position = new Vector3(-9.5f, 0, 0);
            blockRight.position = new Vector3(9.5f, 0, 0);
        }
        else
        {
            blockLeft.position = new Vector3(-11, 0, 0);
            blockRight.position = new Vector3(11, 0, 0);
        }
        
    }
}
