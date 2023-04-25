using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManualSingleton<GameManager>
{
    private TypeAds typeAds;
    
    public void ShowAdsAddCoinWin()
    {
        ShowAds(TypeAds.CoinWin);
    }
    public void ShowAdsAddCoin()
    {
        ShowAds(TypeAds.AddCoin);
    }
    
    public void ShowAds(TypeAds type)
    {
        typeAds = type;
        Admob.Instance.ShowRewardedAd();
    }
    public void GiveRewardAds()
    {
        
        if (typeAds == TypeAds.AddCoin)
        {
            GameController.Instance.ChangeDiamondTxt(50);
        }
        else if (typeAds == TypeAds.CoinWin)
        {
            ColectedDialog.Instance.ShowRandomDiamon(20, 100);
        }
        else if (typeAds == TypeAds.Suggestion)
        {
            GameController.Instance.levelDesign.ActiveHint();
        }
    }
}

public enum TypeAds
{
    AddCoin,
    CoinWin,
    Suggestion
}