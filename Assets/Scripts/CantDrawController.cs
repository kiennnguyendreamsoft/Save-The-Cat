using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantDrawController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.PlayGame)
        {
            Destroy(this.gameObject);
        }
    }
}
