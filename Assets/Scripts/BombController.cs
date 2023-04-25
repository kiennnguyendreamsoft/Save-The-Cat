    using System.Collections;
using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

public class BombController : ObjectBase, IHit
{
    public float time = 5f;
    public float ScaleExplore = 5f;
    new CircleCollider2D collider2D;
    bool hitMagma;
    bool gamestart;
    private bool isUsed;
    public GameObject explosionEffect;
    public TextMeshPro txtTime;

    public void OnHit()
    {
        if (!hitMagma)
        {
            hitMagma = true;
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            collider2D.isTrigger = true;
            CreateExplosion(ScaleExplore * 2);
        }
    }

    protected override void AfterStartGame()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        gamestart = true;
        runMulti = false;
        StartCoroutine(CountTime(5));
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        collider2D = GetComponent<CircleCollider2D>();
    }

    IEnumerator CountTime(int time)
    {
        int count = time;
        txtTime.SetText(count.ToString());
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
            txtTime.SetText(count.ToString());
        }
        gamestart = false;
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        CreateExplosion(ScaleExplore);
    }

    void CreateExplosion(float _scaleSize)
    {
        if(isUsed) return;
        isUsed = true;
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * _scaleSize;
        collider2D.radius *= _scaleSize;
        Destroy(this.gameObject,0.1f);
    }
}
