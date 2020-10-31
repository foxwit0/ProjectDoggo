using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    private void Update()
    {
        //Continuer avec la touche 'Entrer' ou 'Espace'
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            Continue();
        }
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        RespawnSystem.instance.Respawn();
    }

    public void RestartFromZero()
    {
        //Réactivation du temps
        Time.timeScale = 1;

        //Rechargement du niveau actuel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
