using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TerrainController : MonoBehaviour, IHit
{
    public abstract void OnHit();
}
