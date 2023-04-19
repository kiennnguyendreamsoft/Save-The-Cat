using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSelectLevels : MonoBehaviour
{
    public Transform contentHolder;
    public LevelItemPrefab level_item_prefab;
    public List<LevelItemPrefab> levelItems = new List<LevelItemPrefab>();
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Load_lvl_item()
    {
        levelItems.Clear();
        for (int i = 1; i <= DataGame.Instance.lvl_prefabs.Count; i++)
        {
            LevelItemPrefab item = Instantiate(level_item_prefab, contentHolder) as LevelItemPrefab;
            item.lvl_game = i;
            item.Set_Star_lvl(PlayerPrefs.GetInt(DataGame.Key_lvl_star + i, -1));
            levelItems.Add(item);
        }
    }
    public void SetNewGame()
    {
        PlayerPrefs.SetInt(DataGame.Key_lvl_star + 1, 0);
        for (int i = 2; i <= DataGame.Instance.lvl_prefabs.Count; i++)
        {
            PlayerPrefs.SetInt(DataGame.Key_lvl_star + i, -1);
        }
    }
}
