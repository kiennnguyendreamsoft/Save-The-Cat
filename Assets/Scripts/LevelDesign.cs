using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    public GameObject dogDie;
    public int maxPointLineCanDraw = 200;
    private AstarPath astarPath;
    private GameObject hintLine;
    
    private void Start()
    {
        hintLine = this.gameObject.transform.Find("HintLine").gameObject;
        astarPath = transform.Find("Path").GetComponent<AstarPath>();
        StartCoroutine(UpdateScan());
        
        if (DataGame.Instance.lvl_current == 1)
        {
            if (hintLine != null)
            {
                hintLine.SetActive(true);
            }
        }
        else
        {
            if (hintLine != null)
            {
                hintLine.SetActive(false);
            }
        }
    }
    
    IEnumerator UpdateScan()
    {
        while (!GameController.Instance.b_EndGame)
        {
            astarPath.Scan();
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void ActiveHint()
    {
        if (this.gameObject.transform.Find("HintLine") != null)
        {
            DataGame.Instance.ChangeHint(-1);
            if (hintLine != null)
            {
                hintLine.SetActive(true);
            }
        }
    }

    public void UnActiveHint()
    {
        if (hintLine != null)
        {
            hintLine.SetActive(false);
        }
    }

    public void DogDie()
    {
        Instantiate(dogDie, this.transform);
    }
}
