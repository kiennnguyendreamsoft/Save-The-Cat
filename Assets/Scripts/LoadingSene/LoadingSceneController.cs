using System;
using System.Collections;
using System.Collections.Generic;
using TrackingFirebase;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Loadingtime=5;
    float TimeCount;
    public Image loadingValue;
    public RectTransform ghost;
    float LoadingBarSize;
    void Start()
    {
         Application.targetFrameRate = 90;
         loadingValue.fillAmount = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       
        SetBarValue(TimeCount);
        TimeCount += Time.deltaTime*100/Loadingtime;
        if (TimeCount >= 100)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                GameController.Instance.OpenPanelNoInternet();
            }
            if (PlayerPrefs.GetInt("MUSIC",1) == 1)
            {
                GameController.Instance.TurnOnMusic();
            }
            else
            {
                GameController.Instance.TurnOffMusic();
            }
            this.gameObject.SetActive(false);
            TimeCount = 0;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.ShowOpenAds();
    }

    public void SetBarValue(float Percent)
    {
        if (Percent > 0 && Percent <= 100)
        {
            float value = Percent / 100f;
            loadingValue.fillAmount = value;
            ghost.anchoredPosition = new Vector3(820f * value, 80, 0);
        }
    }
}
