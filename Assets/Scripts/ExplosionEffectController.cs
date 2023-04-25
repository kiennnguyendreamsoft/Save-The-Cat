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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("dog"))
        {
            //create smoke
            Instantiate(smokeDog, other.gameObject.transform.position, Quaternion.identity);
            //Lose
            other.gameObject.GetComponent<IHit>().OnHit();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
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
