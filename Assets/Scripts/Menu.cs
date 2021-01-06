using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainText;
    public GameObject controlsText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            quitGame();
        }
    }
    public void playGame()
    {
        Story.started = true;
        SimplyFlying.started = true;
        //#SceneManager.LoadScene("WindKingdom3D");
    }

    public void showControls()
    {
        mainText.SetActive(false);
        controlsText.SetActive(true);
    }

    public void backToMenu()
    {
        mainText.SetActive(true);
        controlsText.SetActive(false);
    }

    public void quitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
    }
}
