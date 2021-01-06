using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            quitGame();
        }
    }
    public void playGame()
    {
        SceneManager.LoadScene("IntroStory");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("WindMaze");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }

    public void quitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
    }
}
