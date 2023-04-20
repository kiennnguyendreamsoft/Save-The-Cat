using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameController : MonoBehaviour
{
    public Sprite win, lose, tryskin;
    public Image titleBG;
    // public TMP_Text titleTxt;
    public GameObject StarEarn1;
    public GameObject StarEarn2;
    public GameObject StarEarn3;
    public TextMeshProUGUI txtGold;
    public void ActiveWin(int StarAmout)
    {
        titleBG.sprite = win;
        // titleTxt.text = "WIN";
        StarEarn1.SetActive(false);
        StarEarn2.SetActive(false);
        StarEarn3.SetActive(false);
        if (StarAmout >= 1)
        {
            StarEarn1.SetActive(true);
        }
        if (StarAmout >= 2)
        {
            StarEarn2.SetActive(true);
        }
        if (StarAmout == 3)
        {
            StarEarn3.SetActive(true);
        }
        
        GameController.Instance.btn_retry_end.gameObject.SetActive(false);
        GameController.Instance.btn_next_end.gameObject.SetActive(true);
        DataGame.Instance.Set_Star_lvl_current(StarAmout);
        if (DataGame.Instance.lvl_current < DataGame.Instance.maxLevel && StarAmout > 0)
        {
            DataGame.Instance.Unlock_Next_lvl();
        }
        DataGame.Instance.ChangeDiamond(10);
        txtGold.SetText("+10");
    }
    public void ActiveLose()
    {
        titleBG.sprite = lose;
        // titleTxt.text = "FAIL";
        StarEarn1.SetActive(false);
        StarEarn2.SetActive(false);
        StarEarn3.SetActive(false);
        GameController.Instance.btn_retry_end.gameObject.SetActive(true);
        GameController.Instance.btn_next_end.gameObject.SetActive(false);
        txtGold.SetText("+0");
    }
}
