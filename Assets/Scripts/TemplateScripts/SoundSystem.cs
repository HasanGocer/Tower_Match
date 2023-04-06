using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioClip mainMusic, coin, finish, objectTouch, wrongObject;

    public void MainMusicPlay()
    {
        mainSource.clip = mainMusic;
        mainSource.Play();
        mainSource.volume = 70;
    }

    public void MainMusicStop()
    {
        mainSource.Stop();
        mainSource.volume = 0;
    }

    public void CallCoinSound()
    {
        mainSource.PlayOneShot(coin);
    }
    public void CallFinishSound()
    {
        mainSource.PlayOneShot(finish);
    }
    public void CallObjectTouchSound()
    {
        mainSource.PlayOneShot(objectTouch);
    }
    public void CallWrongObjectSound()
    {
        mainSource.PlayOneShot(wrongObject);
    }
}
