using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CatController : ObjectBase, IHit
{
    public Animator animator;
    private int indexAnim = 0;
    private bool gameCompleted;
    private bool scary;
    public void OnHit()
    {
        //game over
        if (gameCompleted)
        {
            return;
        }
        SoundManager.Instance.PlaySoundDog();
        RunAnimLose();
        gameCompleted = true;
        if (!GameController.Instance.b_EndGame)
        {
            StartCoroutine(WaitToLose());
        }
    }
    IEnumerator WaitToLose()
    {
        yield return new WaitForSeconds(2f);
        GameController.Instance.ActiveLose();
    }
    
    IEnumerator WaitSmile()
    {
        while (!gameCompleted && !scary)
        {
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            if(gameCompleted || scary) yield break;
            RunAnimSmile();
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        RunAnimIdle();
        //StartCoroutine(WaitSmile());
    }

    protected override void AfterStartGame()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        runMulti = false;
    }

    public void RunAnimIdle()
    {
        animator.Play("idle");
    }

    public void RunAnimScary()
    {
        animator.Play("scary");
    }

    public void RunAnimSmile()
    {
        animator.Play("smile");
    }

    public void RunAnimLose()
    {
        animator.Play("lose");
    }
}
