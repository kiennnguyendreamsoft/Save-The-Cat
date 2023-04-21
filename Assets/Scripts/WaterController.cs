using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : TerrainController
{
    public WaterDropEffect waterDrop;
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
            Instantiate(waterDrop, other.transform.position, Quaternion.identity);
            //Lose
            other.gameObject.GetComponent<IHit>().OnHit();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.gameObject.CompareTag("bee"))
        {
            //create smoke

            //Destroy
            Destroy(other.gameObject);
            Instantiate(waterDrop, other.transform.position, Quaternion.identity);
        }
    }
}
