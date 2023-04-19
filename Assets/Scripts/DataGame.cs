using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    public static DataGame Instance;

    public int _Diamond;
    public int _NoAds;
    public int _Hint;

    public int lvl_current = 0;
    public int indexSkin_current = 1;
    [Header("Level prefabs")]
    public List<LevelDesign> lvl_prefabs;
    public List<Sprite> spritesIdle_dog = new List<Sprite>();
    public List<Sprite> spritesHit_dog = new List<Sprite>();

    public const string Key_new_game = "NewGame";
    public const string Key_lvl_current = "lvl_current";
    public const string Key_lvl_star = "lvl_star_"; //start from lvl 1, not 0
    private void Awake()
    {
        Instance = this;
        _Diamond = PlayerPrefs.GetInt("DiamondValue", 0);
        _NoAds = PlayerPrefs.GetInt("AdsValue", 0);
        _Hint = PlayerPrefs.GetInt("HintValue", 1);
        indexSkin_current = PlayerPrefs.GetInt("_SkinSelected", 1);
    }
    private void Start()
    {
        CheckNewGame();
        lvl_current = Get_lvl_current();
        LoadDataSkin();
        GameController.Instance.ChangeDiamondTxt(_Diamond);
        //ChangeDiamond(20000);
    }
    public void ChangeSkin(int _index)
    {
        indexSkin_current = _index;
        PlayerPrefs.SetInt("_SkinSelected", _index);
        LoadDataSkin();
    }
    void LoadDataSkin()
    {
        Load_Idle_Dog();
        Load_Hit_Dog();
    }
    void Load_Idle_Dog()
    {
        spritesIdle_dog.Clear();
        for (int i = 1; i <= 7; i++)
        {
            Sprite _sprite = Resources.Load<Sprite>("Sprite/Dog/" + indexSkin_current + "/" + i);
            spritesIdle_dog.Add(_sprite);
        }
    }
    void Load_Hit_Dog()
    {
        spritesHit_dog.Clear();
        for (int i = 1; i <= 3; i++)
        {
            Sprite _sprite = Resources.Load<Sprite>("Sprite/Dog_Bite/" + indexSkin_current + "/" + i);
            spritesHit_dog.Add(_sprite);
        }
    }


    public int ChangeDiamond(int value)
    {
        _Diamond += value;
        PlayerPrefs.SetInt("DiamondValue", _Diamond);
        GameController.Instance.ChangeDiamondTxt(value);
        return _Diamond;
    }
    public int ChangeHint(int value)
    {
        _Hint += value;
        PlayerPrefs.SetInt("HintValue", _Hint);
        GameController.Instance.ChangeHintTxt(value);
        return _Hint;
    }
    public void SaveLvlCurrent()
    {
        PlayerPrefs.SetInt(Key_lvl_current, lvl_current);
    }

    #region PlayerPrefs
    void CheckNewGame()
    {
        if (PlayerPrefs.GetInt(Key_new_game) == 0)
        {
            PlayerPrefs.SetInt(Key_new_game, 1);
            Set_lvl_current(1);
            GameController.Instance.panel_Levels.SetNewGame();
        }
    }
    public int Get_lvl_current()
    {
        return PlayerPrefs.GetInt(Key_lvl_current);
    }
    public void Set_lvl_current(int _newLvl)
    {
        lvl_current = _newLvl;
        PlayerPrefs.SetInt(Key_lvl_current, _newLvl);
    }
    public void Set_Star_lvl_current(int _numberStar)
    {
        GameController.Instance.panel_Levels.levelItems[lvl_current - 1].Set_Star_lvl(_numberStar);
        PlayerPrefs.SetInt(Key_lvl_star + lvl_current, _numberStar);
    }
    public void Unlock_Next_lvl()
    {
        GameController.Instance.panel_Levels.levelItems[lvl_current].Set_Star_lvl(0);
        if (lvl_prefabs.Count > lvl_current)
        {
            lvl_current++;
            PlayerPrefs.SetInt(Key_lvl_star + lvl_current, 0);
            Set_lvl_current(lvl_current);
        }
    }
    #endregion PlayerPrefs
}
