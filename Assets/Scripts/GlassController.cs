using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlassController : TerrainController
{
    private float time = 1.5f;
    public Tilemap tileMap;
    public List<Vector3Int> listOfTilePositions = new List<Vector3Int>();
    private int leftCornerX;
    private int leftCornerY;
    public TileBase spriteCrash;
    public override void OnHit()
    {
    }

    private void Start()
    {
        GetGlasses();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "dog")
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
    void GetGlasses()
    {
        for (int x = tileMap.cellBounds.xMin; x < tileMap.cellBounds.xMax; x++)
        {
            for (int y = tileMap.cellBounds.yMin; y < tileMap.cellBounds.yMax; y++)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
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
