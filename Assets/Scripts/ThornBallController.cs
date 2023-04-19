using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornBallController : ObjectBase
{
    protected override void AfterStartGame()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        runMulti = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Destroy something
        if (other.gameObject.tag == "bee")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "dog")
        {
            other.gameObject.GetComponent<IHit>().OnHit();
        }
    }
}
