using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame : MonoBehaviour
{
    public int maxLevel = 60;
    public static DataGame Instance;
    public int _Diamond = 0;
    public int _NoAds;
    public int _Hint;
    public int lvl_current = 0; 
    public int indexSkin_current = 1;
    public const string Key_new_game = "NewGame";
    public const string Key_lvl_current = "lvl_current";
    public const string Key_lvl_star = "lvl_star_"; //start from lvl 1, not 0
    private void Awake()
    {
        Instance = this;
        _NoAds = PlayerPrefs.GetInt("AdsValue", 0);
        _Hint = PlayerPrefs.GetInt("HintValue", 1);
        indexSkin_current = PlayerPrefs.GetInt("SkinSelected", 1);
    }
    private void Start()
    {
        CheckNewGame();
        lvl_current = Get_lvl_current();
    }
    public void ChangeSkin(int _index)
    {
        indexSkin_current = _index;
        PlayerPrefs.SetInt("_SkinSelected", _index);
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
        if (PlayerPrefs.GetInt(Key_lvl_star + lvl_current, 0) < _numberStar)
        {
            PlayerPrefs.SetInt(Key_lvl_star + lvl_current, _numberStar);
        }
    }
    public void Unlock_Next_lvl()
    {
        GameController.Instance.panel_Levels.levelItems[lvl_current].Set_Star_lvl(0);
        if (maxLevel > lvl_current)
        {
            lvl_current++;
            PlayerPrefs.SetInt(Key_lvl_star + lvl_current, 0);
            Set_lvl_current(lvl_current);
        }
    }
    #endregion PlayerPrefs
}
