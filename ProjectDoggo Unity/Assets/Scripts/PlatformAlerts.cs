using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlatformAlerts : MonoBehaviour
{
    const int NUMBER_OF_ALERTS = 16;

    //Variables relatives au joueur
    private float playerYPosition;
    private PlayerMovement player;

    //Variables relatives à la détection des plateformes
    [SerializeField] private float startDetectionPosOffset = -18f;
    [SerializeField] private float detectionDistance = 30f;

    //Variables relatives au alertes
    [SerializeField] private GameObject platformAlertsGO = null;
    private Image[] platformAlerts;
    private int platformLayerMask;
    [SerializeField] private Color alertInitialColor = Color.black;
    [SerializeField] private Color alertFinalColor = Color.black;
    private bool[] isDisabling = new bool[NUMBER_OF_ALERTS];

    void Start()
    {
        player = PlayerMovement.instance;

        //Récupération des GO de chaque alerte plateforme
        platformAlerts = platformAlertsGO.GetComponentsInChildren<Image>();

        //Récupération du layer Platform
        platformLayerMask = LayerMask.GetMask("Platform");
    }

    void Update()
    {
        playerYPosition = player.transform.position.y;        

        for(int i = 0; i < platformAlerts.Length; i++) //Boucle entre l'ensemble des alertes plateforme
        {
            Vector2 raycastStartPos = new Vector2(i - (platformAlerts.Length / 2f) + 0.5f, playerYPosition + startDetectionPosOffset); //Calcul de la position de départ du raycast de détection plateforme
            RaycastHit2D hit = Physics2D.Raycast(raycastStartPos, Vector2.down, detectionDistance, platformLayerMask); //Raycast de détection plateforme
            Debug.DrawRay(raycastStartPos, Vector2.down * detectionDistance, Color.red); //Affichage du raycast sur la scène

            if(hit && !platformAlerts[i].enabled)
            {
                ShowAlert(i);
            } 
            else if(!hit && platformAlerts[i].enabled)
            {                
                HideAlert(i);
            }

            //Si l'alerte est affichée, ajustement de sa couleur selon l'éloignement de la plateforme
            if(platformAlerts[i].enabled && !isDisabling[i])
            {
                float distanceFromStartPos = Mathf.Abs(hit.point.y - raycastStartPos.y);
                UpdatePlatformAlertUI(i, distanceFromStartPos, detectionDistance);
            }
            
        }
    }

    private void ShowAlert(int alertIndex)
    {
        platformAlerts[alertIndex].enabled = true;
        platformAlerts[alertIndex].GetComponent<Animator>().SetTrigger("Enable");
    }

    private void HideAlert(int alertIndex)
    {   
        isDisabling[alertIndex] = true;
        platformAlerts[alertIndex].GetComponent<Animator>().SetTrigger("Disable");
        StartCoroutine((DisableImage(alertIndex)));
    }

    public void HideAllAlerts()
    {
        for(int i = 0; i < platformAlerts.Length; i++) //Boucle entre l'ensemble des alertes plateforme
        {
            platformAlerts[i].enabled = false;
        }
    }

    IEnumerator DisableImage(int _alertIndex)
    {
        yield return new WaitForSeconds(0.2f);
        platformAlerts[_alertIndex].enabled = false;
        isDisabling[_alertIndex] = false;
    }

    private void UpdatePlatformAlertUI(int alertIndex, float distance, float maxDistance)
    {
        float colorRatio = distance / maxDistance;
        platformAlerts[alertIndex].color = Color.Lerp(alertFinalColor, alertInitialColor, colorRatio); //Dégradé de couleur     
    }   

}
