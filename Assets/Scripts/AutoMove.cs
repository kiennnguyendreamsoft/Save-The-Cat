using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutoMove : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    private float speedX = 1.3f, speeY = 1.3f;
    private Vector2 direction;
    private bool nextDirection;
    private void Start()
    {
        nextDirection = true;
        if (speedX > 0 || speeY > 0)
        {
            StartCoroutine(OnAuto());
        }
    }

    IEnumerator OnAuto()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (nextDirection)
            {
                yield return new WaitForSeconds(Random.Range(1f, 1f));
                nextDirection = false;
                int random = Random.Range(0, 4);
                if (random == 0)
                {
                    if (minY == 0) random = 1;
                    direction = Vector2.down;
                }
                if (random == 1)
                {
                    if (maxY == 0) random = 2;
                    direction = Vector2.up;
                }
                if (random == 2)
                {
                    if (minX == 0) random = 3;
                    direction = Vector2.left;
                }
                if (random == 3)
                {
                    direction = Vector2.right;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (speedX > 0 || speeY > 0)
        {
            
            Vector2 me = this.transform.position;
            me.x += speedX * Time.fixedDeltaTime * 0.1f * direction.x;
            me.y += speedX * Time.fixedDeltaTime * 0.1f * direction.y;
            if (me.x > maxX || me.x < minX || me.y > maxY || me.y < minY)
            {
                nextDirection = true;
                return;
            }
            this.transform.position = me;
        }
        
    }
    
    
}
