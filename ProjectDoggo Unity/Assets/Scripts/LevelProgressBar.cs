using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    //Variables de GO à renseigner sur l'Inspector
    [SerializeField] private RectTransform progressBarUI = null;
    [SerializeField] private RectTransform playerUI = null;
    [SerializeField] private Slider plasmaUI = null;
    [SerializeField] private Transform plasmaTransform = null;

    //Variables utilisées pour la barre de progression
    private float levelHeight;
    private float progressBarHeight;

    //Variables pour le plasma
    private Image plasma;

    //Récupération du Transform component du joueur
    private Transform playerTransform;

    void Start()
    {        
        //Initialisation des variables liées à la barre de progression
        levelHeight = LevelManager.instance.levelHeight;
        Image progressBar = progressBarUI.GetComponent<Image>();
        progressBarHeight = progressBarUI.sizeDelta.y;

        //Récupération du Transform component du joueur
        playerTransform = PlayerMovement.instance.transform;
    }

    void Update()
    {
        //Rafraichissement de la position du joueur dans la barre de progression
        float newPlayerPosUI = Mathf.Clamp((playerTransform.position.y / levelHeight) * progressBarHeight, -progressBarHeight + playerUI.sizeDelta.y / 2f, 0f);
        playerUI.anchoredPosition = new Vector2(0f, newPlayerPosUI);

        //Rafraichissement du plasma dans la barre de progression
        plasmaUI.value = -plasmaTransform.position.y / levelHeight;
    }
}
