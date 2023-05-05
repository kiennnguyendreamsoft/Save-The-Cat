using System;
using System.Collections;
using UnityEngine;

public class Admob : ManualSingleton<Admob>
{
#if UNITY_ANDROID
    private const string AppOpenAdUnitId = "015d06a7abb45815";
    private string bannerAdUnitId = "74635e0ddee677bf";
    private string adUnitId = "04ed20453d7dae93";
    private string idReward = "4ab7fae3428c6312";
#elif UNITY_IPHONE
    private const string AppOpenAdUnitId = "aee489c590e360cc";
    private string bannerAdUnitId = "cd1e131ca18ff2ac";
    private string adUnitId = "010aa24db52b8115";
    private string idReward = "e087615ebc15e790";
#else
    private const string AppOpenAdUnitId = "YOUR_IOS_AD_UNIT_ID";
    private string bannerAdUnitId = "YOUR_IOS_BANNER_AD_UNIT_ID";
    private string adUnitId = "YOUR_IOS_AD_UNIT_ID";
    private string idReward = "ca-app-pub-3940256099942544/5224354917";
#endif

    private bool NoAds;
    IEnumerator Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // Hiển thị trình gỡ lỗi dàn xếp
        };
        MaxSdk.SetTestDeviceAdvertisingIdentifiers(new string[]{"3d9b1cb5-c1d3-4bd1-6e6a-10ca1ad1abe1"} );
        MaxSdk.InitializeSdk();
        CheckNoAds();
        yield return new WaitForSeconds(1);
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        yield return new WaitForSeconds(4);
        InitializeBannerAds();
        ShowBanner();
        yield return new WaitForSeconds(2);
        InitializeInterstitialAds();
        InitializeRewardedAds();
    }

    public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    }

    public void ShowAdIfReady()
    {
        if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
        {
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        }
    }
    
    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        //MaxSdk.SetBannerExtraParameter(bannerAdUnitId, "adaptive_banner", "true");
        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.clear);

        MaxSdkCallbacks.Banner.OnAdLoadedEvent      += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent  += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent     += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent    += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent   += OnBannerAdCollapsedEvent;
    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) {}

    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)  {}

    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

    public void ShowBanner()
    {
        if(NoAds) return;
        MaxSdk.ShowBanner(bannerAdUnitId);
    }

    public void HideBanner()
    {
        MaxSdk.HideBanner(bannerAdUnitId);
    }

    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
    
        // Load the first interstitial
        LoadInterstitial();
    }

    private bool isLoadInter;
    private void LoadInterstitial()
    {
        if(isLoadInter) return;
        StartCoroutine(DelayLoadInter());
    }

    IEnumerator DelayLoadInter()
    {
        isLoadInter = true;
        yield return new WaitForSeconds(2);
        MaxSdk.LoadInterstitial(adUnitId);
        isLoadInter = false;
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        if (checkAdsWin)
        {
            GameController.Instance.PlayGame_NextLvl();
        }
        else
        {
            GameController.Instance.Reload_Game();
        }
        LoadInterstitial();
    }

    public void ShowInterstitialLose()
    {
        if(NoAds) return;
        if ( MaxSdk.IsInterstitialReady(adUnitId) )
        {
            checkAdsWin = false;
            MaxSdk.ShowInterstitial(adUnitId);
        }
        else
        {
            GameController.Instance.Reload_Game();
            LoadInterstitial();
        }
    }
    
    public void ShowInterstitialWin()
    {
        if (GameController.Instance.countPlay >= 3)
        {
            if(NoAds) return;
            if ( MaxSdk.IsInterstitialReady(adUnitId) )
            {
                checkAdsWin = true;
                GameController.Instance.countPlay = 0;
                MaxSdk.ShowInterstitial(adUnitId);
            }
            else
            {
                GameController.Instance.PlayGame_NextLvl();
                LoadInterstitial();
            }
        }
        else
        {
            GameController.Instance.PlayGame_NextLvl();
        }
    }

    private bool checkAdsWin;
    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
                
        // Load the first rewarded ad
        LoadRewardedAd();
    }

    private bool isLoadVideo;
    private void LoadRewardedAd()
    {
        if(isLoadVideo) return;
        StartCoroutine(DelayLoadVideo());
    }

    IEnumerator DelayLoadVideo()
    {
        isLoadVideo = true;
        yield return new WaitForSeconds(2);
        MaxSdk.LoadRewardedAd(idReward);
        yield return new WaitForSeconds(1);
        isLoadVideo = false;
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        GameManager.Instance.GiveRewardAds();
        LoadRewardedAd();
    }

    private void OnRewardedAdRevenuePaidEvent(string idReward, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }

    public void ShowAdsReward()
    {
        if (MaxSdk.IsRewardedAdReady(idReward))
        {
            MaxSdk.ShowRewardedAd(idReward);
        }
        else
        {
            LoadRewardedAd();
        }
    }

    public void CheckNoAds()
    {
        NoAds= (PlayerPrefs.GetInt("NO_ADS", 0) == 1);
        if (NoAds)
        {
            HideBanner();
            GameController.Instance.HaveNoAds();
        }
    }
}
