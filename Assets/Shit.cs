using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit: ObjectBase
{
    // Start is called before the first frame update
    protected override void AfterStartGame()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        runMulti = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "dog")
        {
            other.gameObject.GetComponent<CatController>().RunAnimLose_shit();
            other.gameObject.GetComponent<CatController>().OnHit();
        }
    }
}
