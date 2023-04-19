using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : ManualSingleton<SoundManager>
{
    public AudioSource music;
    public AudioSource soundBee;
    public AudioSource soundDog;
    public AudioSource soundWin;
    public AudioSource soundLose;
    public AudioSource soundButton;
    public AudioSource soundGift;
    public AudioSource soundClock;
    public AudioClip[] listSoundDog;
    private int checkSound;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        checkSound = PlayerPrefs.GetInt("SOUND",1);
        if (checkSound == 1)
        {
            GameController.Instance.TurnOnSound();
        }
        else
        {
            GameController.Instance.TurnOffSound();
        }
    }

    public void MuteMusic()
    {
        music.Stop();
        PlayerPrefs.SetInt("MUSIC",0);
    }

    public void UnMuteMusic()
    {
        music.Play();
        PlayerPrefs.SetInt("MUSIC",1);
    }

    public void MuteSound()
    {
        checkSound = 0;
        PlayerPrefs.SetInt("SOUND",0);
    }

    public void UnMuteSound()
    {
        checkSound = 1;
        PlayerPrefs.SetInt("SOUND",1);
    }
    
    public void PlaySoundBee()
    {
        if(checkSound == 0) return;
        soundBee.Play();
    }

    public void StopSoundBee()
    {
        if(checkSound == 0) return;
        soundBee.Stop();
    }

    public void PlaySoundDog()
    {
        if(checkSound == 0) return;
        if(listSoundDog.Length >= DataGame.Instance.indexSkin_current && listSoundDog[DataGame.Instance.indexSkin_current - 1]!=null)
            soundDog.clip = listSoundDog[DataGame.Instance.indexSkin_current - 1];
        soundDog.Play();
    }

    public void PlaySoundButton()
    {
        if(checkSound == 0) return;
        soundButton.Play();
    }

    public void PlaySoundWin()
    {
        if(checkSound == 0) return;
        soundWin.Play();
    }

    public void PlaySoundLose()
    {
        if(checkSound == 0) return;
        soundLose.Play();
    }

    public void PlaySoundGift()
    {
        if(checkSound == 0) return;
        soundGift.Play();
    }
    
    public void PlaySoundClock(float volume)
    {
        if(checkSound == 0) return;
        soundClock.volume = volume;
        soundClock.Play();
    }
}
