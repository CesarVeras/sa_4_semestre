using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource sound;
    public static SoundManager manager = null;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        sound.clip = audioClip;
        sound.Play();
    }

    public void PlayMusic()
    {
        sound.Play();
    }
}
