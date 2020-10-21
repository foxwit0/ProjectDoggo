using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform progressBarUI = null;
    [SerializeField] private RectTransform playerUI = null;
    [SerializeField] private GameObject plasmaUI = null;

    private float levelHeight;
    private float progressBarHeight;
    private Image plasma;

    private Transform playerPosGO;

    void Start()
    {
        levelHeight = LevelManager.instance.levelHeight;

        Image progressBar = progressBarUI.GetComponent<Image>();
        progressBarHeight = progressBarUI.sizeDelta.y;

        plasma = plasmaUI.GetComponent<Image>();

        playerPosGO = PlayerMovement.instance.transform;
    }

    void Update()
    {
        //Rafraichissement de la position du joueur dans la barre de progression
        float newPlayerPosUI = Mathf.Clamp((playerPosGO.position.y / levelHeight) * progressBarHeight, -progressBarHeight + playerUI.sizeDelta.y / 2f, 0f);
        playerUI.anchoredPosition = new Vector2(0f, newPlayerPosUI);

    }
}
