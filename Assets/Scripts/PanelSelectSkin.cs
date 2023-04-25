using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelSelectSkin : ManualSingleton<PanelSelectSkin>
{
    // Start is called before the first frame update
    public RectTransform _Content;
    public SellectSkinCat obj;
    private List<SellectSkinCat> listSellectSkin = new List<SellectSkinCat>();
    

    protected override void Awake()
    {
        base.Awake();
        PlayerPrefs.SetInt("Unlock_1", 1);
        Init();
    }
    
    public void Init()
    {
        if (listSellectSkin.Count == 0)
        {
            for (int i = 1; i < 8; i++)
            {
                SellectSkinCat clone = Instantiate(obj, _Content);
                clone.Init(i);
                listSellectSkin.Add(clone);
            }
        }
    }
    
    public void UnlockSkin(SellectSkinCat sellect)
    {
        int _Diamond = PlayerPrefs.GetInt("DiamondValue", 0);
        if (_Diamond > sellect.cost)
        {
            PlayerPrefs.SetInt("Unlock_"+sellect.id, 1);
            sellect.Unlock();
            ChoseSkin(sellect);
            GameController.Instance.ChangeDiamondTxt(-1 * sellect.cost);
        }
    }
    
    public void ChoseSkin(SellectSkinCat sellect)
    {
        foreach (SellectSkinCat item in listSellectSkin)
        {
            item.DeSelect();
        }
        sellect.Select();
        DataGame.Instance.ChangeSkin(sellect.id);
    }
    

}
