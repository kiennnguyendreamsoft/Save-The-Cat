using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneController : MonoBehaviour
{
    public TMP_Text lvl_txt;
    [HideInInspector] public int maxPointLineCanDraw;
    public Button Btn_Hint;
    public Button Btn_Shop;
    private void OnEnable()
    {
        Btn_Shop.gameObject.SetActive(false);
        if (DataGame.Instance.lvl_current == 1)
        {
            Btn_Hint.gameObject.SetActive(false);
        }
        else
        {
            Btn_Hint.gameObject.SetActive(true);
        }

        if (lvl_txt != null)
        {
            lvl_txt.text = "Level " + DataGame.Instance.lvl_current.ToString();
        }
    }
    private void OnDisable() {
          Btn_Shop.gameObject.SetActive(true);
    }


}
