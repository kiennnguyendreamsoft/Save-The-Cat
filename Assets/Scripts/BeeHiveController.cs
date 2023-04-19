using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BeeHiveController : ObjectBase
{
    public Transform SpawmPos;
    [FormerlySerializedAs("bee")] public GhostController ghost;
    [FormerlySerializedAs("beeAI")] public GhostAI ghostAI;
    public int BeeAmount;
    bool spawmReady = true;
    // Update is called once per frame
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }
    IEnumerator SpawmBee()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.Instance.PlaySoundBee();
        Vector2 pos = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        if (BeeAmount > 3)
        {
            GhostController ghost = Instantiate(this.ghost, SpawmPos) as GhostController;
            ghost.transform.localPosition = pos;
            spawmReady = true;
            BeeAmount--;
        }
        else
        {
            GhostAI ghost = Instantiate(ghostAI, SpawmPos) as GhostAI;
            ghost.transform.localPosition = pos;
            spawmReady = true;
            BeeAmount--;
        }
    }
    void SpawmBeeCount()
    {
        if (BeeAmount > 0 && spawmReady)
        {
            spawmReady = false;
            StartCoroutine(SpawmBee());
        }
    }
    protected override void AfterStartGame()
    {
        SpawmBeeCount();
    }
}
