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
        SoundManager.Instance.PlaySoundDogLose();
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
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        RunAnimIdle();
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
        if(scary) return;
        scary = true;
        SoundManager.Instance.PlaySoundGhostSpawn();
        animator.Play("scary");
    }
    
    public void RunAnimLose()
    {
        SoundManager.Instance.PlaySoundDogLose();
        animator.Play("lose");
    }
    
    public void RunAnimWin()
    {
        SoundManager.Instance.PlaySoundDogWin();
        animator.Play("win");
    }
}
