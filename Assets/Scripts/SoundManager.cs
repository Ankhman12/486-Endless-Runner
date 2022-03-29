using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource levelMusic;

    public AudioSource ambientElectricalSound;

    public AudioSource jumpSound;

    public AudioSource hitSound;

    public AudioSource pickupSound;

    public AudioSource symptomSound;

    public AudioSource deathSound;

    //Singleton Stuff
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }

}
