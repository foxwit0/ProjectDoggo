using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void PlayTime()
    {
        //Jouer le temps
        Time.timeScale = 1;
    }

    public void Restart()
    {
        PlayTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        PlayTime();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT !!");
        Application.Quit();
    }
}
