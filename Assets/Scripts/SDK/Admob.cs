// using System;
// using GoogleMobileAds.Api;
// using UnityEngine;
// using AdPosition = GoogleMobileAds.Api.AdPosition;
// using AdSize = GoogleMobileAds.Api.AdSize;
// using InterstitialAd = GoogleMobileAds.Api.InterstitialAd;
//
// public class Admob : ManualSingleton<Admob>
// {
//     public DataAds dataAds = new DataAds();
//     
//     public void SetDataAds(DataAds data)
//     {
//         dataAds = data;
//     }
//     public void Init()
//     {
//         // Initialize the Google Mobile Ads SDK.
//         MobileAds.Initialize((InitializationStatus initStatus) =>
//         {
//             // This callback is called once the MobileAds SDK is initialized.
//         });
//         LoadAppOpenAd();
//         LoadInterstitialAd();
//         LoadRewardedAd();
//     }
//
//     private DateTime _expireTime;
//     private AppOpenAd appOpenAd;
//     
//     public bool IsAdAvailable
//     {
//         get
//         {
//             return appOpenAd != null
//                    && appOpenAd.IsLoaded()
//                    && DateTime.Now < _expireTime;
//         }
//     }
//     
//     /// <summary>
//     /// Loads the app open ad.
//     /// </summary>
//     public void LoadAppOpenAd()
//     {
//         // Clean up the old ad before loading a new one.
//         if (appOpenAd != null)
//         {
//             appOpenAd.Destroy();
//             appOpenAd = null;
//         }
//
//         Debug.Log("Loading the app open ad.");
//
//         // Create our request used to load the ad.
//         var adRequest = new AdRequest.Builder().Build();
//
//         // send the request to load the ad.
//         AppOpenAd.Load(SDKManager.Instance.infoSdk.key_open_app, ScreenOrientation.Portrait, adRequest,
//             (AppOpenAd ad, LoadAdError error) =>
//             {
//                 // If the operation failed, an error is returned.
//                 if (error != null || ad == null)
//                 {
//                     Debug.LogError("App open ad failed to load an ad with error : " + error);
//                     return;
//                 }
//
//                 // If the operation completed successfully, no error is returned.
//                 Debug.Log("App open ad loaded with response : " + ad.GetResponseInfo());
//
//                 // App open ads can be preloaded for up to 4 hours.
//                 _expireTime = DateTime.Now + TimeSpan.FromHours(4);
//
//                 appOpenAd = ad;
//                 RegisterReloadHandler(appOpenAd);
//             });
//     }
//     
//     private void RegisterReloadHandler(AppOpenAd ad)
//     {
//         // Raised when the ad closed full screen content.
//         ad.OnAdFullScreenContentClosed += LoadAppOpenAd;
//         ad.OnAdFullScreenContentFailed += delegate{LoadAppOpenAd();  };
//     }
//
//     public void ShowAppOpenAd()
//     {
//         if (IsAdAvailable && appOpenAd.CanShowAd())
//         {
//             Debug.Log("Showing app open ad.");
//             appOpenAd.Show();
//         }
//         else
//         {
//             Debug.LogError("App open ad is not ready yet.");
//         }
//     }
//     
//     BannerView _bannerView;
//
//     /// <summary>
//     /// Creates a 320x50 banner at top of the screen.
//     /// </summary>
//     public void ShowBanner()
//     {
//         if(_bannerView == null)
//         {
//             Debug.Log("Creating banner view");
//
//             // If we already have a banner, destroy the old one.
//             if (_bannerView != null)
//             {
//                 HideBanner();
//             }
//
//             // Create a 320x50 banner at top of the screen
//             _bannerView = new BannerView(dataAds.banner, AdSize.IABBanner, AdPosition.Bottom);
//             _bannerView.OnBannerAdLoaded += () =>
//             {
//                 AdsManager.Instance.loadedBanner = true;
//             };
//         }
//         // create our request used to load the ad.
//         var adRequest = new AdRequest.Builder()
//             .AddKeyword("unity-admob-sample")
//             .Build();
//         // send the request to load the ad.
//         Debug.Log("Loading banner ad.");
//         _bannerView.LoadAd(adRequest);
//     }
//     public void HideBanner()
//     {
//         if (_bannerView != null)
//         {
//             _bannerView.Destroy();
//             _bannerView = null;
//         }
//     }
//     
//     
//     private InterstitialAd interstitialAd;
//
//     /// <summary>
//     /// Loads the interstitial ad.
//     /// </summary>
//     public void LoadInterstitialAd()
//     {
//         // Clean up the old ad before loading a new one.
//         if (interstitialAd != null)
//         {
//             interstitialAd.Destroy();
//             interstitialAd = null;
//         }
//
//         Debug.Log("Loading the interstitial ad.");
//
//         // create our request used to load the ad.
//         var adRequest = new AdRequest.Builder()
//             .AddKeyword("unity-admob-sample")
//             .Build();
//
//         // send the request to load the ad.
//         InterstitialAd.Load(dataAds.interstitial, adRequest,
//             (InterstitialAd ad, LoadAdError error) =>
//             {
//                 // if error is not null, the load request failed.
//                 if (error != null || ad == null)
//                 {
//                     Debug.LogError("interstitial ad failed to load an ad " + "with error : " + error);
//                     return;
//                 }
//                 Debug.Log("Interstitial ad loaded with response : "+ ad.GetResponseInfo());
//
//                 interstitialAd = ad;
//                 RegisterReloadHandler(interstitialAd);
//             });
//     }
//     private void RegisterReloadHandler(InterstitialAd ad)
//     {
//         // Raised when the ad closed full screen content.
//         ad.OnAdFullScreenContentClosed += delegate { LoadInterstitialAd(); };
//         
//         // Raised when the ad failed to open full screen content.
//         ad.OnAdFullScreenContentFailed += (AdError error) => { LoadInterstitialAd(); };
//     }
//     
//     public bool ShowIntertitial()
//     {
//         Debug.LogError("Check show inter admob");
//         if (interstitialAd != null && interstitialAd.CanShowAd())
//         {
//             Debug.Log("Showing interstitial ad.");
//             interstitialAd.Show();
//             return true;
//         }
//         else
//         {
//             LoadInterstitialAd();
//             Debug.LogError("Interstitial ad is not ready yet.");
//             return false;
//         }
//     }
//     
//     
//     private RewardedAd rewardedAd;
//
//     /// <summary>
//     /// Loads the rewarded ad.
//     /// </summary>
//     public void LoadRewardedAd()
//     {
//         // Clean up the old ad before loading a new one.
//         if (rewardedAd != null)
//         {
//             rewardedAd.Destroy();
//             rewardedAd = null;
//         }
//
//         Debug.Log("Loading the rewarded ad.");
//
//         // create our request used to load the ad.
//         var adRequest = new AdRequest.Builder().Build();
//
//         // send the request to load the ad.
//         RewardedAd.Load(dataAds.reward, adRequest,
//             (RewardedAd ad, LoadAdError error) =>
//             {
//                 // if error is not null, the load request failed.
//                 if (error != null || ad == null)
//                 {
//                     Debug.LogError("Rewarded ad failed to load an ad " +
//                                    "with error : " + error);
//                     return;
//                 }
//
//                 Debug.Log("Rewarded ad loaded with response : "
//                           + ad.GetResponseInfo());
//
//                 rewardedAd = ad;
//                 RegisterReloadHandler(rewardedAd);
//             });
//     }
//     private void RegisterReloadHandler(RewardedAd ad)
//     {
//         // Raised when the ad closed full screen content.
//         ad.OnAdFullScreenContentClosed +=LoadRewardedAd;
//         
//         // Raised when the ad failed to open full screen content.
//         ad.OnAdFullScreenContentFailed += (AdError error) =>{ LoadRewardedAd(); };
//     }
//     
//     public bool ShowRewardedVideo()
//     {
//         Debug.LogError("Check show reward admob");
//         if (rewardedAd != null)
//         {
//             rewardedAd.Show((Reward reward) =>
//             {
//                 AdsManager.Instance.RewardedSucces();
//             });
//             return true;
//         }
//         else
//         {
//             LoadRewardedAd();
//             return false;
//         }
//     }
// }
