using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    protected new Rigidbody2D rigidbody2D;
    protected bool runMulti;
    protected virtual void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        runMulti = true;
    }
    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        if (GameController.Instance.PlayGame == true)
        {
            if (runMulti)
            {
                AfterStartGame();
            }
        }
    }
    protected abstract void AfterStartGame();
}
