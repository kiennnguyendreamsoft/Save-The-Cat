using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CatController : ObjectBase, IHit
{
    public SkeletonMecanim skeletonMecanim;
    public Animator animator;
    private int indexAnim = 0;
    private bool gameCompleted;
    private bool scary;
    private bool lose;
    public void OnHit()
    {
        //game over
        if (gameCompleted)
        {
            return;
        }
        SoundManager.Instance.PlaySoundDogLose();
        gameCompleted = true;
        if (!GameController.Instance.b_EndGame)
        {
            GameController.Instance.Lose();
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        skeletonMecanim.initialSkinName = "skin" + DataGame.Instance.indexSkin_current;
        skeletonMecanim.Initialize(true);
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
        if(scary || gameCompleted) return;
        scary = true;
        SoundManager.Instance.PlaySoundCatScary();
        animator.Play("scary");
    }
    
    public void RunAnimLose()
    {
        if(lose) return;
        SoundManager.Instance.PlaySoundDogLose();
        animator.Play("lose");
    }
    
    public void RunAnimLose_shit()
    {
        lose = true;
        SoundManager.Instance.PlaySoundDogLose();
        animator.Play("lose_shit");
    }
    
    public void RunAnimLose_u()
    {
        lose = true;
        SoundManager.Instance.PlaySoundDogLose();
        animator.Play("lose_u");
    }
    
    public void RunAnimWin()
    {
        SoundManager.Instance.PlaySoundDogWin();
        animator.Play("win");
    }
    
}
