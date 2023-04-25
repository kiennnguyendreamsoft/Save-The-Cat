using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TrackingFirebase;
//using TrackingFirebase;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [Header("Text")]
    public TextMeshProUGUI DiamondTxt;

    [Header("Buttons Main Menu")]
    public Button btn_Play;
    public Button btn_Level;
    public Button btn_Skin;
    public Button btn_Gift;
    public Button btn_Setting;
    public Button btn_NoAds;
    public Button btn_Shop;
    //public Button btn_Plus; //vẫn là shop nhưng khác cái hình
    [Header("Buttons Setting Menu")]
    public Button btn_Music_on;
    public Button btn_Music_off;
    public Button btn_Sound_on;
    public Button btn_Sound_off;
    public Button btn_Close_setting;
    [Header("Buttons Game Scene")]
    public Button btn_home_game;
    public Button btn_retry_game;

    public Button btn_hint;
    [Header("Buttons Game End")]
    public Button btn_home_end;
    public Button btn_retry_end;
    public Button btn_next_end;
    public Button btn_Ads_end;

    [Header("Transform Panels")]
    public GameObject panel_StartGame;
    public GameObject panel_Setting;
    public PanelSelectLevels panel_Levels;
    public GameObject panel_Skin;
    public GameObject panel_Gift;
    public GameSceneController panel_Game;
    public EndGameController panel_EndGame;
    public Transform gameHolder;
    public DrawManager drawManager;
    
    [Header("InGame")]
    public bool PlayGame;
    public bool b_EndGame;
    public Image ProcessDraw;
    public List<GameObject> stars = new List<GameObject>();
    float TimeToWin = 10f;
    public TMPro.TMP_Text time_txt;

    public LevelDesign levelDesign;
    public GameObject buyNoAds;

    public GameObject panelNoInternet;
    public ParticleSystem particleWin;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Main Menu
        btn_Play.onClick.AddListener(PlayGame_atLastLvl);
        btn_Level.onClick.AddListener(OpenPanelSelectLevel);
        btn_Skin.onClick.AddListener(OpenPanelSkin);
        btn_Gift.onClick.AddListener(OpenPanelGift);
        btn_Setting.onClick.AddListener(OpenPanelSetting);
        btn_NoAds.onClick.AddListener(BuyNoAds);
        btn_Shop.onClick.AddListener(OpenPanelShop);
        //btn_Plus.onClick.AddListener(OpenPanelShop);
        //Setting Menu
        btn_Music_on.onClick.AddListener(TurnOffMusic);
        btn_Music_off.onClick.AddListener(TurnOnMusic);
        btn_Sound_on.onClick.AddListener(TurnOffSound);
        btn_Sound_off.onClick.AddListener(TurnOnSound);
        btn_Close_setting.onClick.AddListener(BackToMainMenu);
        //Game Scene
        btn_home_game.onClick.AddListener(BackToMainMenu);
        btn_retry_game.onClick.AddListener(
            delegate
            {
                Reload_Game();
                FirebaseUtils.Instance.RetryStage(DataGame.Instance.lvl_current);
            });
        //Game End
        btn_home_end.onClick.AddListener(BackToMainMenu);
        btn_retry_end.onClick.AddListener(Reload_Game);
        btn_next_end.onClick.AddListener(PlayGame_NextLvl);
        btn_Ads_end.onClick.AddListener(WatchAdsToEarn);
        btn_hint.onClick.AddListener(ShowHind);
        panel_Levels.Load_lvl_item();
        drawManager.enabled = false;
        
        ChangeDiamondTxt(0);
    }

    public void ShowHind()
    {
        GameManager.Instance.ShowAds(TypeAds.Suggestion);
    }
    public void CountTimeOut(int time)
    {
        StopAllCoroutines();
        StartCoroutine(Count(time));
    }

    IEnumerator Count(int time)
    {
        TimeToWin = time;
        time_txt.text = TimeToWin.ToString();
        time_txt.gameObject.SetActive(true);
        while (TimeToWin > 0)
        {
            yield return new WaitForSeconds(1);
            TimeToWin--;
            time_txt.text = TimeToWin.ToString();
            SoundManager.Instance.PlaySoundClock(0.5f-TimeToWin*0.02f);
        }
        yield return new WaitForSeconds(1);
        time_txt.gameObject.SetActive(false);
        ActiveWin();
    }

    public void PlayGameAfterDraw(){
        PlayGame = true;
        levelDesign.UnActiveHint();
        btn_hint.gameObject.SetActive(false);
        CountTimeOut(12);
    }

    public void OpenPanelNoInternet()
    {
        panelNoInternet.SetActive(true);
    }
    public void HaveNoAds()
    {
        btn_NoAds.gameObject.SetActive(false);
        //noAdsInShop.SetActive(false);
        buyNoAds.SetActive(false);
    }
    public void PlayGame_atLastLvl()
    {
        SoundManager.Instance.PlaySoundButton();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OpenPanelNoInternet();
            return;
        }
        panel_StartGame.SetActive(false);
        UnactivePanel_WhenPlayGame();
        Reload_Game();
    }
    public void PlayGame_NextLvl()
    {
        PlayGame_AtLvl(DataGame.Instance.lvl_current);
    }
    public void PlayGame_AtLvl(int _index)
    {
        DataGame.Instance.lvl_current = _index;
        UnactivePanel_WhenPlayGame();
        Reload_Game();
    }
    public void Reload_Game()
    {
        StopAllCoroutines();
        drawManager.enabled = false;
        PlayGame = false;
        b_EndGame = false;
        time_txt.gameObject.SetActive(false);
        if (gameHolder.childCount > 0)
        {
            foreach (LevelDesign item in gameHolder.GetComponentsInChildren<LevelDesign>())
            {
                Destroy(item.gameObject);
            }
            StartCoroutine(createNewLvl());
        }
        else
        {
            StartCoroutine(createNewLvl());
        }
        drawManager.enabled = true;
        panel_EndGame.gameObject.SetActive(false);
        DataGame.Instance.SaveLvlCurrent();
        FirebaseUtils.Instance.Start_Lvl(DataGame.Instance.lvl_current);
        StartCoroutine(StartAdsInter());
    }
    IEnumerator createNewLvl()
    {
        yield return new WaitForEndOfFrame();
        SoundManager.Instance.StopSoundBee();
        btn_hint.gameObject.SetActive(true);
        GameObject obj = Resources.Load<GameObject>("Level/"+DataGame.Instance.lvl_current);
        GameObject lvl = Instantiate(obj, gameHolder);
        levelDesign = lvl.GetComponent<LevelDesign>();
        panel_Game.maxPointLineCanDraw = levelDesign.maxPointLineCanDraw;
        ChangeProcessDraw(1f);
        
    }
    public void OpenPanelSelectLevel()
    {
        SoundManager.Instance.PlaySoundButton();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OpenPanelNoInternet();
            return;
        }
        panel_Levels.gameObject.SetActive(true);
    }
    public void OpenPanelSkin()
    {
        SoundManager.Instance.PlaySoundButton();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OpenPanelNoInternet();
            return;
        }
        panel_Skin.SetActive(true);
    }
    public void OpenPanelGift()
    {
        SoundManager.Instance.PlaySoundButton();
        panel_Gift.SetActive(true);
    }
    public void OpenPanelSetting()
    {
        SoundManager.Instance.PlaySoundButton();
        panel_Setting.SetActive(true);
    }
    public void BuyNoAds()
    {
        //mua No Ads
        //InAppProcess.Instance.InitBuyIAP("removeAds");
    }
    public void EndGame()
    {
        panel_EndGame.gameObject.SetActive(true);
    }
    public void OpenPanelShop()
    {
        
    }
    public void BackToMainMenu()
    {
        StopAllCoroutines();
        SoundManager.Instance.StopSoundBee();
        SoundManager.Instance.PlaySoundButton();
        panel_StartGame.SetActive(true);
        panel_Setting.SetActive(false);
        panel_Levels.gameObject.SetActive(false);
        panel_Skin.SetActive(false);
        panel_Gift.SetActive(false);
        panel_Game.gameObject.SetActive(false);
        panel_EndGame.gameObject.SetActive(false);
        drawManager.enabled = false;
        if (gameHolder.childCount > 0)
        {
            foreach (LevelDesign item in gameHolder.GetComponentsInChildren<LevelDesign>())
            {
                Destroy(item.gameObject);
            }
        }
    }
    public void WatchAdsToEarn()
    {
        SoundManager.Instance.PlaySoundButton();
        Debug.Log("Watch Ads");
        btn_Ads_end.interactable = false;
    }
    void CleanGameScene()
    {
        if (gameHolder.childCount > 0)
        {
            foreach (LevelDesign item in gameHolder.GetComponentsInChildren<LevelDesign>())
            {
                Destroy(item.gameObject);
            }
        }
    }
    void UnactivePanel_WhenPlayGame()
    {
        panel_StartGame.SetActive(false);
        panel_Levels.gameObject.SetActive(false);
        panel_Setting.SetActive(false);
        panel_Gift.SetActive(false);
        panel_Skin.SetActive(false);
        panel_EndGame.gameObject.SetActive(false);
        panel_Game.gameObject.SetActive(false);
        panel_Game.gameObject.SetActive(true);
    }

    public void ChangeProcessDraw(float newValue)
    {
        if (newValue > 1f)
        {
            newValue = 1f;
        }
        else if (newValue < 0)
        {
            newValue = 0;
        }
        ProcessDraw.transform.localScale = new Vector3(newValue, 1f, 1f);
        if (newValue >= 0.66f)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
        else if (newValue >= 0.33f && newValue < 0.66f)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(false);
        }
        else if (newValue > 0 && newValue < 0.33f)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }
        else if (newValue <= 0)
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }
    }
    void ActiveWin()
    {
        PlayGame = false;
        b_EndGame = true;
        SoundManager.Instance.StopSoundBee();
        levelDesign.ActiveWin();
        particleWin.Play();
        StartCoroutine(WaitShowWin());
    }

    IEnumerator WaitShowWin()
    {
        yield return new WaitForSeconds(3f);
        int count = 0;
        foreach (GameObject item in stars)
        {
            if (item.activeInHierarchy)
            {
                count++;
            }
        }
        panel_EndGame.gameObject.SetActive(true);
        panel_EndGame.ActiveWin(count);
        btn_Ads_end.interactable = true;
        ShowRewardDiamond(10);
        FirebaseUtils.Instance.StageWin(DataGame.Instance.lvl_current);
        SoundManager.Instance.PlaySoundWin();
    }

    public void Lose()
    {
        PlayGame = false;
        foreach (CatController cat in levelDesign.listCat)
        {
            cat.RunAnimLose();
        }
        StopAllCoroutines();
        StartCoroutine(WaitShowLose());
    }
    
    IEnumerator WaitShowLose()
    {
        yield return new WaitForSeconds(2f);
        ActiveLose();
    }
    
    public void ActiveLose()
    {
        if(!b_EndGame)SoundManager.Instance.PlaySoundLose();
        b_EndGame = true;
        SoundManager.Instance.StopSoundBee();
        panel_EndGame.gameObject.SetActive(true);
        panel_EndGame.ActiveLose();
        FirebaseUtils.Instance.FailLevelStage(DataGame.Instance.lvl_current);
    }

    IEnumerator StartAdsInter()
    {
        yield return new WaitForSeconds(0.2f);
        Admob.Instance.ShowInterstitial();
    }
    
    public void TurnOnMusic()
    {
        //SoundManager.Instance.PlaySoundButton();
        btn_Music_off.gameObject.SetActive(false);
        SoundManager.Instance.UnMuteMusic();
    }
    public void TurnOffMusic()
    {
        //SoundManager.Instance.PlaySoundButton();
        btn_Music_off.gameObject.SetActive(true);
        SoundManager.Instance.MuteMusic();
    }
    public void TurnOnSound()
    {
        btn_Sound_off.gameObject.SetActive(false);
        SoundManager.Instance.UnMuteSound();
    }
    public void TurnOffSound()
    {
        btn_Sound_off.gameObject.SetActive(true);
        SoundManager.Instance.MuteSound();
    }

    public void ChangeDiamondTxt(int value)
    {
        int diamond = PlayerPrefs.GetInt("DiamondValue");
        diamond += value;
        PlayerPrefs.SetInt("DiamondValue", diamond);
        DiamondTxt.SetText(diamond.ToString());
    }

    public void ShowRewardDiamond(int value)
    {
        ColectedDialog.Instance.ShowDialog(value);
        ChangeDiamondTxt(value);
    }
    public void ChangeHintTxt(int value)
    {
        // HintTxt.text = PlayerPrefs.GetInt("HintValue").ToString();
        // if (ColectedDialog.Instance != null && value>0)
        // {
        //     ColectedDialog.Instance.ShowDialog(Resources.Load<Sprite>("NewUi/Starter-pack_0004_Layer-7"), value);
        // }
    }
}
