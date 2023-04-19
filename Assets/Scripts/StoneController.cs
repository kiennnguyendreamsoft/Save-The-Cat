using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : ObjectBase
{
    protected override void AfterStartGame()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        runMulti = false;
    }
}
