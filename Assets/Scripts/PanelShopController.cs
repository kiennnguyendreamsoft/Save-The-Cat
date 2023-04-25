using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelShopController : MonoBehaviour
{

    public static PanelShopController Instance;
    public Button Btn_WatchDiamond;
    public Button Btn_watchHint;

    // public List<Button> Btn_BuyDiamond;
    public Button Btn_BuyHint1;
    public Button Btn_BuyHint3;
    public Button Btn_BuyHint6;
    public Button Btn_BuyHint9;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Btn_BuyHint1.onClick.AddListener(delegate { BuyHint(50, 1); });
        Btn_BuyHint3.onClick.AddListener(delegate { BuyHint(150, 3); });
        Btn_BuyHint6.onClick.AddListener(delegate { BuyHint(300, 6); });
        Btn_BuyHint9.onClick.AddListener(delegate { BuyHint(450, 9); });
    }

    public void BuyHint(int diamondPrice, int value)
    {
        if (diamondPrice <= PlayerPrefs.GetInt("DiamondValue", 20))
        {
            DataGame.Instance.ChangeHint(value);
            Debug.Log(diamondPrice);
        }
    }
}
