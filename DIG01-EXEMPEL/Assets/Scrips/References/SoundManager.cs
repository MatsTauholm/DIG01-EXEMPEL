using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //public static SoundManager instance;
    static AudioSource audioSource;


    private void Start()
    {
        //if (instance != null && instance != this)
        //{
        //    Destroy(instance);
        //}
        //else
        //{
        //    instance = this;
        //}
    }

    static public void PlaySound(AudioClip clipToPlay)
    {
        audioSource.PlayOneShot(clipToPlay);
    }

}
