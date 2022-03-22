using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonActions : MonoBehaviour
{
    [SerializeField] private GameObject gameInformationPanel;

    [SerializeField] private GameObject mainMenuPanel;

    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject menuCamera;

    /** Reset the scene since we are doing a one-scene approach */
    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(exitCode);
#endif
    }

    public void StartGame()
    {
        gameInformationPanel.SetActive(true);
        menuCamera.SetActive(false);

        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);

        GameManager.Instance.gameRunning = true;
        Time.timeScale = 1; //TODO stop using timescale
    }
}
