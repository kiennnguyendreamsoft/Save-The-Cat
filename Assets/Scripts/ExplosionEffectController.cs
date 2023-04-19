using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectController : MonoBehaviour
{
    public SmokeEffect smokeEffect;
    public SmokeEffect smokeDog;
    
    public void destroySelf()
    {
        Destroy(this.gameObject);
    }
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "dog")
    //     {
    //         //create smoke

    //         //Lose
    //         other.gameObject.GetComponent<IHit>().OnHit();
    //         other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //         other.gameObject.GetComponent<Collider2D>().enabled = false;
    //     }
    //     if (other.gameObject.tag == "bee")
    //     {
    //         //create smoke

    //         //Destroy
    //         Destroy(other.gameObject);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("dog"))
        {
            //create smoke
            Instantiate(smokeDog, other.gameObject.transform.position, Quaternion.identity);
            //Lose
            other.gameObject.GetComponent<IHit>().OnHit();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (other.gameObject.CompareTag("bee"))
        {
            //create smoke
            if(smokeEffect == null) return;
            Instantiate(smokeEffect, other.gameObject.transform.position, Quaternion.identity);
            //Destroy
            Destroy(other.gameObject);
        }
    }
}
