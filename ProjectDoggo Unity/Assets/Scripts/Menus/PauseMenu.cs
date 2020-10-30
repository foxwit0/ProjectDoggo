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
        //Recharger le niveau actuel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        PlayTime();
        //Charger le menu principal
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT !!");
        Application.Quit();
    }
}
