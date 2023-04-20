using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManualSingleton<GameManager>
{
    private TypeAds typeAds;
    public void SetTypeAds(TypeAds type)
    {
        typeAds = type;
    }
    public void GiveRewardAds()
    {
        if (typeAds == TypeAds.AddCoin)
        {
            DataGame.Instance.ChangeDiamond(50);
        }
        else if (typeAds == TypeAds.CoinWin)
        {
            ColectedDialog.Instance.ShowRandomDiamon(20, 100);
        }
        else if (typeAds == TypeAds.Suggestion)
        {
            DataGame.Instance.ChangeHint(1);
        }
    }
}

public enum TypeAds
{
    AddCoin,
    CoinWin,
    Suggestion
}