using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : ObjectBase
{
    public float forceWind;
    new Collider2D collider2D;
    protected override void Start()
    {
        base.Start();
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
    }
    protected override void AfterStartGame()
    {
        collider2D.enabled = true;
        runMulti = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "bee")
        {
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * forceWind);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "bee")
        {
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
