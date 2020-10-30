using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        //Ouvrir le menu pause avec la touche echap
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);

            //Arrêt du temps
            Time.timeScale = 0;
        }
    }
}
