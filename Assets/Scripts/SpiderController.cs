using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : ObjectBase
{
    public float speedSpider;
    Transform line;
    float originY;
    Animator animator;
    bool gotSomething;
    protected override void AfterStartGame()
    {
        if (!gotSomething)
        {
            transform.position += Vector3.down * Time.deltaTime * speedSpider;
        }
        else
        {
            if (transform.position.y <= originY)
            {
                transform.position += Vector3.up * Time.deltaTime * speedSpider;
                if(line != null) line.position += Vector3.up * Time.deltaTime * speedSpider;
            }
        }
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }
    protected override void Start()
    {
        base.Start();
        originY = transform.position.y;
        animator = GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("bee"))
        {
            if (other.gameObject.CompareTag("line"))
            {
                other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                line = other.transform;
            }
            
            if (other.gameObject.CompareTag("dog"))
            {
                other.gameObject.GetComponent<IHit>().OnHit();
            }
            gotSomething = true;
            animator.Play("catch");
        }
    }
}
