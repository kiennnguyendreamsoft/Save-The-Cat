using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeController : ObjectBase
{
    private List<Rigidbody2D> dogs = new List<Rigidbody2D>();
    private List<Rigidbody2D> lines = new List<Rigidbody2D>();
    bool gotLines;
    public float forceHole;
    private bool active;
    protected override void AfterStartGame()
    {
        active = true;
    }

    protected override void Start()
    {
        base.Start();
        dogs.Clear();
        foreach (CatController _dog in FindObjectsOfType<CatController>())
        {
            dogs.Add(_dog.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void FixedUpdate()
    {
        if(!active) return;
        foreach (Rigidbody2D item in dogs)
        {
            Vector2 vec = transform.position - item.transform.position;
            item.AddForce(vec * forceHole);
        }
        if (!gotLines) GetLines();
        foreach (Rigidbody2D item in lines)
        {
            Vector2 vec = transform.position - item.transform.position;
            item.AddForce(vec * forceHole * 2f);
        }
    }

    private void GetLines()
    {
        foreach (Line _line in FindObjectsOfType<Line>())
        {
            lines.Add(_line.gameObject.GetComponent<Rigidbody2D>());
        }
        gotLines = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "line")
        {
            if (other.gameObject.tag == "dog")
            {
                other.gameObject.GetComponent<IHit>().OnHit();
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            if (other.gameObject.tag == "bee")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
