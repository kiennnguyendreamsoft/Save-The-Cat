using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public SkeletonAnimation spinAnim;
    public List<CatController> cats = new List<CatController>();
    public float BeeSpeed;
    private float BeeSpeedBack;
    new Rigidbody2D rigidbody2D;
    private Vector3 DirTarget;
    private bool isBack;
    private float timeBack;
    private int Left_or_Right;
    private bool needToDown;
    private bool needToUp;
    private bool needToLeft;
    // Start is called before the first frame update
    void Start()
    {
        spinAnim.AnimationState.SetAnimation(0, "animation", true);
        rigidbody2D = GetComponent<Rigidbody2D>();
        foreach (CatController cat in FindObjectsOfType<CatController>())
        {
            cats.Add(cat);
        }

        RandomTarget();
        Left_or_Right = Random.Range(-2, 2);
        BeeSpeedBack = BeeSpeed * 2;
    }

    public void RandomTarget()
    {
        if (cats.Count == 1)
        {
            DirTarget = cats[0].transform.position;
        }
        else
        {
            DirTarget = cats[Random.Range(0, cats.Count)].transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.b_EndGame)
        {
            BeeLogic();
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
        }
    }

    private void BeeLogic()
    {
        DirTarget = (cats[0].transform.position - transform.position).normalized;
        float distance = Vector3.Distance(cats[0].transform.position, transform.position);
        for (int i = 0; i < cats.Count; i++)
        {
            if (Vector3.Distance(cats[i].transform.position, transform.position) < distance)
            {
                DirTarget = (cats[i].transform.position - transform.position).normalized;
                distance = Vector3.Distance(cats[i].transform.position, transform.position);
            }
            //up and down
            if (transform.position.y - cats[i].transform.position.y < 0)
            {
                needToUp = true;
                needToDown = false;
            }
            else if (transform.position.y - cats[i].transform.position.y > 0)
            {
                needToUp = false;
                needToDown = true;
            }
            //left and right
            if (transform.position.x - cats[i].transform.position.x < 1)
            {
                needToLeft = false;
            }
            else if (transform.position.x - cats[i].transform.position.x > 1)
            {
                needToLeft = true;
            }
        }

        if (!isBack)
        {
            rigidbody2D.velocity = DirTarget * BeeSpeed;
        }
        else
        {
            Vector3 needToGo = transform.position + Vector3.right * Left_or_Right;
            if (needToDown)
            {
                needToGo += Vector3.down;
            }
            else if (needToUp)
            {
                needToGo += Vector3.up;
            }

            if (needToLeft)
            {
                needToGo += Vector3.left * 3f;
            }

            Vector3 targetBack = (cats[0].transform.position - needToGo).normalized;
            rigidbody2D.velocity = -targetBack * BeeSpeedBack;
            if (timeBack > 0)
            {
                timeBack -= Time.deltaTime;
            }
            else
            {
                isBack = false;
            }
        }

        if (DirTarget.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private float m_Thrust = 300f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "bee")
        {
            rigidbody2D.velocity = Vector2.zero;
            isBack = true;
            timeBack = 0.2f;
            if (other.gameObject.tag == "line")
            {
                Vector2 direction = (DirTarget - transform.position).normalized;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * m_Thrust);
                rigidbody2D.AddForce(-1*direction*10);
                RandomTarget();
                foreach (CatController cat in cats)
                {
                    cat.RunAnimScary();
                }
            }
            if (other.gameObject.tag == "dog")
            {
                other.gameObject.GetComponent<IHit>().OnHit();
            }
            if (other.gameObject.tag == "wall")
            {
                Left_or_Right *= -1;
            }
            if (other.gameObject.tag == "ground")
            {
                rigidbody2D.velocity += Vector2.right * Left_or_Right;
            }
        }
    }
}
