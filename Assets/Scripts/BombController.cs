using System.Collections;
using System.Collections.Generic;
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

    public void OnHit()
    {
        if (!hitMagma)
        {
            hitMagma = true;
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            collider2D.isTrigger = true;
            CreateExplosion(ScaleExplore);
        }
    }

    protected override void AfterStartGame()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        gamestart = true;
        runMulti = false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        collider2D = GetComponent<CircleCollider2D>();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (gamestart)
        {

            time -= Time.deltaTime;
            if (time <= 0)
            {
                gamestart = false;
                rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                collider2D.isTrigger = true;
                CreateExplosion(1.5f);
            }
        }
    }
    
    void CreateExplosion(float _scaleSize)
    {
        if(isUsed) return;
        isUsed = true;
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * _scaleSize;
        Destroy(this.gameObject,0.1f);
    }
}
