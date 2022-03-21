using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private float distanceTraveled = 0f;
    [SerializeField] private GameObject player;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        CalculateDistanceTraveled();
    }

    void CalculateDistanceTraveled()
    {
        distanceTraveled = player.transform.position.z - transform.position.z;
    }
}