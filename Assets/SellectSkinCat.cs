using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellectSkinCat : MonoBehaviour
{
    public int id;
    public int cost = 200;
    public Button btnBuy;
    public Button btnSellect;
    public Image img;
    public TextMeshProUGUI txtCost;
    public GameObject sellect;
    
    void Start()
    {
        btnBuy.onClick.AddListener(OnClickBuy);
        btnSellect.onClick.AddListener(OnClickSellect);
    }

    public void Init(int index)
    {
        id = index;
        img.sprite = Resources.Load<Sprite>("SkinIcon/" + id);
        sellect.SetActive(PlayerPrefs.GetInt("SkinSelected", 1) == id);
        img.color = Color.black;
        txtCost.SetText(cost.ToString());
        btnBuy.gameObject.SetActive(true);
        btnSellect.enabled = false;
        if (PlayerPrefs.GetInt("Unlock_" + id, 0) == 1)
        {
            Unlock();
        }

        btnBuy.interactable = false;
    }

    public void Unlock()
    {
        btnBuy.gameObject.SetActive(false);
        btnSellect.enabled = true;
        img.color = Color.white;
    }
    public void OnClickBuy()
    {
        PanelSelectSkin.Instance.UnlockSkin(this);
    }

    public void OnClickSellect()
    {
        PanelSelectSkin.Instance.ChoseSkin(this);
    }

    public void DeSelect()
    {
        sellect.SetActive(false);
    }
    
    public void Select()
    {
        sellect.SetActive(true);
    }
}
