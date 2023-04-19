using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HintController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelShop;
    public Button Btn_hint;
    void Start()
    {
        Btn_hint.onClick.AddListener(ActiveHint);
    }
    
    public void ActiveHint()
    {
        if (PlayerPrefs.GetInt("HintValue", 1) > 0)
        {
            GameController.Instance.levelDesign.ActiveHint();
            Btn_hint.gameObject.SetActive(false);
            //FirebaseUtils.Instance.UseSuggestion(DataGame.Instance.lvl_current);
        }
        else
        {
            panelShop.SetActive(true);
        }
        

    }
}
