using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    //Variables de GO à renseigner sur l'Inspector
    [SerializeField] private LevelProgressBarUI levelProgressBarUI = null;
    [SerializeField] private Transform plasmaTransform = null;

    //Variables utilisées pour la barre de progression
    private float levelHeight;

    //Variables pour le plasma
    private Image plasma;

    //Récupération du Transform component du joueur
    private Transform playerTransform;

    void Start()
    {        
        //Initialisation des variables liées à la barre de progression
        levelHeight = LevelManager.instance.levelHeight;

        //Récupération du Transform component du joueur
        playerTransform = PlayerMovement.instance.transform;
    }

    void Update()
    {
        //Rafraichissement de la position du joueur dans la barre de progression
        levelProgressBarUI.UpdatePlayerPosition(playerTransform, levelHeight);

        //Rafraichissement du plasma dans la barre de progression
        levelProgressBarUI.UpdatePlasmaPosition(plasmaTransform, levelHeight);
    }
}
