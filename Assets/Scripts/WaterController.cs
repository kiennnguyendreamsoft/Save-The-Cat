using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : TerrainController
{
    public SmokeEffect smokeDog;
    public override void OnHit()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("dog"))
        {
            //create smoke
            Instantiate(smokeDog, other.gameObject.transform.position, Quaternion.identity);
            //Lose
            other.gameObject.GetComponent<CatController>().OnHit();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.gameObject.CompareTag("bee"))
        {
            //Destroy
            Destroy(other.gameObject);
        }
    }
}
