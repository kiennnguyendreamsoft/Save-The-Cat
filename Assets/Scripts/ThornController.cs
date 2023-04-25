using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornController : MonoBehaviour
{
    public SmokeEffect smokeEffect;
    public SmokeEffect smokeDog;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
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
            //Destroy
            Destroy(other.gameObject);
            if(smokeEffect == null) return;
            Instantiate(smokeEffect, other.gameObject.transform.position, Quaternion.identity);
        }
    }
}
