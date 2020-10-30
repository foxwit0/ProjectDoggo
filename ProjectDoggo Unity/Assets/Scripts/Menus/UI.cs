using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Update()
    {
        //Ouvrir le menu pause avec la touche echap
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseMenu.activeSelf)
            {
                //Ouverture du menu Pause
                pauseMenu.SetActive(true);
                //Arrêt du temps
                Time.timeScale = 0;
            }
            else
            {
                //Fermeture du menu Pause
                pauseMenu.SetActive(false);
                //Jouer le temps
                Time.timeScale = 1;
            }
        }
    }
}
