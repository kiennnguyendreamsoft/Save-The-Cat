using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Spine.Unity;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostAI : MonoBehaviour
{
    public Animator spinAnim;
    public AIPath aiPath;
    public CatController target;
    private float speed = 200f;
    public float nextWaypointDistance = 3l;
    private Path path;
    private int currentWaypoint;
    private bool reachedEndOfPath;
    private Seeker seeker;
    private Rigidbody2D rb;
    private List<CatController> cats = new List<CatController>();
    private bool auto = true, autoAttack = true;
    private Vector2 vectorBack;
    void Start()
    {
        spinAnim.Play("animation", -1,0);
        speed = Random.Range(150, 200);
        foreach (CatController _dog in FindObjectsOfType<CatController>())
        {
            cats.Add(_dog);
        }
        RandomTarget();
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        auto = true;
        StartCoroutine(UpdatePath());
    }

    public void RandomTarget()
    {
        if (cats.Count == 1)
        {
            target = cats[0];
        }
        else
        {
            target = cats[Random.Range(0, cats.Count)];
        }
    }
    
    IEnumerator UpdatePath(){
        while(!GameController.Instance.b_EndGame)
        {
            yield return new WaitForSeconds(0.2f);
            if (auto)
            {
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
            }
        }
        
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        while(GameController.Instance.b_EndGame)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if(path == null) return;

        if (auto)
        {
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
            rb.velocity = direction * speed * Time.deltaTime;
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            rb.velocity = vectorBack *2;
        }
        
        
        if (target.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    private float m_Thrust = 1000f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("dog"))
        {
            other.gameObject.GetComponent<IHit>().OnHit();
        }   
        if (other.gameObject.CompareTag("line"))
        {
            auto = false;
            vectorBack = (transform.position - target.transform.position).normalized;
            RandomTarget();
            foreach (CatController cat in cats)
            {
                cat.RunAnimScary();
            }

            StartCoroutine(AutoOn());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("dog") || col.gameObject.CompareTag("line"))
        {
            if (autoAttack)
            {
                spinAnim.Play("fly_2", -1, 0);
                autoAttack = false;
                StartCoroutine(AutoOnAnimAttack());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("dog") || other.gameObject.CompareTag("line"))
        {
            if (autoAttack)
            {
                spinAnim.Play("fly_2", -1, 0);
                autoAttack = false;
                StartCoroutine(AutoOnAnimAttack());
            }
        }
    }

    IEnumerator AutoOn()
    {
        yield return new WaitForSeconds(0.6f);
        auto = true;
    }
    
    IEnumerator AutoOnAnimAttack()
    {
        yield return new WaitForSeconds(0.6f);
        autoAttack = true;
    }
}
