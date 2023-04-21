using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlassController : TerrainController
{
    private float time = 1.5f;
    private Tilemap tileMap;
    public List<Vector3Int> listOfTilePositions = new List<Vector3Int>();
    private int leftCornerX;
    private int leftCornerY;
    public TileBase spriteCrash;
    private bool active;
    public override void OnHit()
    {
    }

    private void Start()
    {
        GetGlasses();
    }

    private void Update()
    {
        if (active)
        {
            time -= Time.deltaTime;
            if (time > 0 && time <= 0.75f)
            {
                SetSprite();
            }
            if (time <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameController.Instance.PlayGame)
        {
            active = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameController.Instance.PlayGame)
        {
            active = true;
        }
    }

    void GetGlasses()
    {
        tileMap = GetComponent<Tilemap>();
        for (int x = -100; x < 100; x++)
        {
            for (int y = -100; y < 100; y++)
            {
                Vector3Int localPlace = new Vector3Int(x, y, 0);
                if (tileMap.HasTile(localPlace))
                {
                    listOfTilePositions.Add(new Vector3Int(x - leftCornerX, y - leftCornerY, 0));
                }
            }
        }
    }
    void SetSprite()
    {
        for (int i = 0; i < listOfTilePositions.Count; i++)
        {
            tileMap.SetTile(listOfTilePositions[i], spriteCrash);
        }
    }
}
