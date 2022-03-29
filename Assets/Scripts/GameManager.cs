using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    /* Enumeration for different types of symptoms that can be experienced by the player */
    public enum Symptom { Tremors, Vision };

    [Header("Game Information")]
    [SerializeField] private int playerLives = 3;
    [SerializeField] private float distanceTraveled = 0f;

    /** Bool to tell if game is currently running - false to start because of menu */
    public bool gameRunning;

    [Header("Object References")]
    [SerializeField] private GameObject player;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private SymptomNumbness symptomNumbness;

    [Header("UI Objects")]
    [SerializeField] private TextMeshProUGUI distanceAmtText;
    [SerializeField] private TextMeshProUGUI livesAmtText;
    [SerializeField] private GameObject gameOverPanel;

    /** List of symptoms currently active for the player */
    private List<Symptom> activeSymptoms;
    /** Static instance of a game manager in the scene that can be used by other classes to interact with game state*/
    private static GameManager _instance;

    /** Static function that returns the singleton instance of the GameManager */
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        //Using time scale right now, eventually should use GameManager.Instance.gameRunning in player movement script
        Time.timeScale = 0;

        //Ensure instance is set at the very beginning of the scene
        _instance = this;
    }

    void Start()
    {
        //Initialize fields
        activeSymptoms = new List<Symptom>();

        //Initialize UI
        livesAmtText.text = playerLives.ToString();
    }

    void Update()
    {
        //Calculate Distance Traveled
        distanceTraveled = player.transform.position.z - transform.position.z;

        //Update UI
        distanceAmtText.text = string.Format("{0} μm", distanceTraveled.ToString("F1"));
    }

    public void AddSymptom(Symptom symptom)
    {
        if (!activeSymptoms.Contains(symptom))
        {
            activeSymptoms.Add(symptom);

            switch (symptom)
            {
                case Symptom.Tremors:
                    cameraShake.StartShaking();
                    break;
                case Symptom.Vision:
                    symptomNumbness.StartNumbness();
                    break;
                default:
                    break;
            }
        }
    }

    public void RemoveSymptom(Symptom symptom)
    {
        if (activeSymptoms.Contains(symptom))
        {
            activeSymptoms.Remove(symptom);
            switch (symptom)
            {
                case Symptom.Tremors:
                    cameraShake.StopShaking();
                    break;
                case Symptom.Vision:
                    symptomNumbness.StopNumbness();
                    break;
                default:
                    break;
            }
        }
    }

    public void AddRandomSymptom() 
    {
        // Select a random symptom
        Symptom randomSymptom = (Symptom)Random.Range(0, 2);
        Debug.Log("Adding random symptom: " + randomSymptom);
        // Add it to the list of active symptoms
        AddSymptom(randomSymptom);
    }

    public void DamagePlayer()
    {
        //Subtract life
        playerLives--;

        //Update UI
        livesAmtText.text = playerLives.ToString();

        //Make visual effect changes
        switch (playerLives)
        {
            case 2:
                //TODO Stop external lightning effect 
                break;
            case 1:
                //TODO Stop internal electricity effect
                break;
            case 0:
                //End Game
                //TODO - any visual effects here
                StopGame();
                break;
        }
    }

    void StopGame()
    {
        //Might need to make this a coroutine and do it after a certain amt of time if we want a death visual
        Time.timeScale = 0;
        gameRunning = false;
        gameOverPanel.SetActive(true);
    }
}