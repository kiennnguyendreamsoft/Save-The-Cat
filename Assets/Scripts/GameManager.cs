using System.Collections;
using System.Collections.Generic;
using TrackingFirebase;
using UnityEngine;

public class GameManager : ManualSingleton<GameManager>
{
    private TypeAds typeAds;

    public void ShowOpenAds()
    {
        Admob.Instance.ShowAdIfReady();
        FirebaseUtils.Instance.OpenApp();
    }
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
        Admob.Instance.ShowAdsReward();
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