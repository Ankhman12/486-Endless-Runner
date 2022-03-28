using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource levelMusic;

    [SerializeField] AudioSource ambientElectricalSound;

    [SerializeField] AudioSource jumpSound;

    [SerializeField] AudioSource hitSound;

    //Singleton Stuff
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}
