using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropEffect : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endAnim());
    }
    IEnumerator endAnim()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        Destroy(this.gameObject);
    }
}
