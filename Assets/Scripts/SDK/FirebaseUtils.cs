using UnityEngine;
using System;
using System.Collections;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.Messaging;


namespace TrackingFirebase
{
    public class FirebaseProperty
    {
        public const string PlayerId = "player_id";
    }

    public class FirebaseEvent
    {
        public const string BtnUseSuggestion = "btn_use_suggestion_{0}";
        public const string BtnRetry = "click_retry_level_{0}";
        
        public const string RewardAttempt = "reward_attempt";
        public const string ShowRewardSuccess = "af_reward";
        public const string RewardCompleted = "reward_completed";
        
        public const string StartLevel = "play_level_{0}";
        public const string FailLevel = "lose_level_{0}";
        public const string WinLevel = "win_level_{0}";
        public const string OpenApp = "open_app";
        
        public const string ShowInterSuccess = "af_inters";
        public const string CodeShowInter = "inter_attempt";
        
    }

    public class FirebaseUtils : ManualSingleton<FirebaseUtils>
    {

        public static bool Initialized { get; private set; }

        private bool _isDebugMode;

        private string _firebaseToken;
        
        // When the app starts, check to make sure that we have
        // the required dependencies to use Firebase, and if not,
        // add them if possible.
        protected override void Awake()
        {
            base.Awake();
        }
        IEnumerator Start()
        {
            yield return new WaitForSeconds(4f);
            InitFirebase(false);
        }
        
        public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
          UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
        }
        
        public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e) {
          UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
        }

        public void InitFirebase(bool isDebugMode)
        {
            Initialized = false;
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    Initialized = true;

                    this._isDebugMode = isDebugMode;

                    // Init Crashlytics
                    Application.RequestAdvertisingIdentifierAsync(
                        (string advertisingId, bool trackingEnabled, string error) =>
                        {
                            
                        }
                    );
                    GetFirebaseToken();
                    var id = SystemInfo.deviceUniqueIdentifier;

                    SetUserProperty(FirebaseProperty.PlayerId, string.IsNullOrEmpty(id) ? null : id);
                    SetUserId(string.IsNullOrEmpty(id) ? null : id);
                    Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                    Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

                }
                else
                {
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }

        private void GetFirebaseToken()
        {
            FirebaseMessaging.GetTokenAsync().ContinueWith(
                task =>
                {
                    if (!(task.IsCanceled || task.IsFaulted) && task.IsCompleted)
                    {
                        Debug.Log($"FIREBASE ID TOKEN: {task.Result}");
                        _firebaseToken = task.Result;
                    }
                });
        }

        public void SetUserId(string userId)
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning("[Firebase] USER_ID : " + userId);
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.SetUserId(userId);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void SetUserProperty(string name, string property)
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning("[Firebase] PROPERTY : NAME " + name + " VALUE:" + property);
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                if (Initialized)
                {
                    FirebaseAnalytics.SetUserProperty(name.ToLower(), property.ToLower());
                }
                else
                {
                }
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region Level
        
        public void Start_Lvl(int level)
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] Start level : {0}", level));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.StartLevel, level));
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public void StageWin(int level)
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] Win level : {0}", level));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.WinLevel, level));
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public void FailLevelStage(int level)
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] Faild level : {0}", level));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.FailLevel, level));
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void UseSuggestion(int level)
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] Click Button Watch Ads Skip level : {0}", level));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.BtnUseSuggestion, level));
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public void RetryStage(int level)
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] Click reset level : {0}", level));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.BtnRetry, level));
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void OpenApp()
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("OpenApp"));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.OpenApp));
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void RewardCompletedStage()
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] Reward Completed Stage"));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(FirebaseEvent.RewardCompleted);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        
        public void RewardAttempt()
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] RewardAttempt"));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(FirebaseEvent.RewardAttempt);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public void ShowRewardSuccesStage()
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] SShowRewardSucces"));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(FirebaseEvent.ShowRewardSuccess);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public void ShowInterSuccess()
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] ShowInterSuccess"));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.ShowInterSuccess));
#endif

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void CodeShowInterSuccess()
        {
            if (this._isDebugMode)
            {
                Debug.LogWarning(string.Format("[Firebase] ShowInterSuccess"));
                return;
            }

            try
            {
#if TRACKING_FIREBASE
                FirebaseAnalytics.LogEvent(string.Format(FirebaseEvent.CodeShowInter));
#endif

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        #endregion
    }
    
    


}