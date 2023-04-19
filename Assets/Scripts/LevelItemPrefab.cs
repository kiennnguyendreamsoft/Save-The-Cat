using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelItemPrefab : MonoBehaviour
{
    private Button thisBtn;
    public TMP_Text lvl_txt;
    public GameObject lvl_unlock;
    public GameObject lvl_lock;
    //public Image img_lvl;
    public GameObject[] stars;
    [HideInInspector] public int lvl_game;
    // Start is called before the first frame update
    void Start()
    {
        thisBtn = GetComponent<Button>();
        //Check lvl done or undone
        lvl_txt.text = "Lv" + lvl_game;
        thisBtn.onClick.AddListener(OpenGame);
    }

    public void Set_Star_lvl(int _numberStar)
    {
        if (_numberStar == -1)
        {
            //lvl_lock.SetActive(true);
            //lvl_unlock.SetActive(true);
            lvl_lock.SetActive(true);
            lvl_unlock.SetActive(false);
        }
        else if (_numberStar >= 0)
        {
            lvl_unlock.SetActive(true);
            lvl_lock.SetActive(false);
            if (_numberStar >= 1)
            {
                stars[0].SetActive(true);
            }
            if (_numberStar >= 2)
            {
                stars[1].SetActive(true);
            }
            if (_numberStar >= 3)
            {
                stars[2].SetActive(true);
            }
        }
    }
    void OpenGame()
    {
        if (!lvl_lock.activeInHierarchy)
        {
            GameController.Instance.PlayGame_AtLvl(lvl_game);
        }
        else
        {
            Debug.LogFormat("Unlock not yet");
        }
    }
}
