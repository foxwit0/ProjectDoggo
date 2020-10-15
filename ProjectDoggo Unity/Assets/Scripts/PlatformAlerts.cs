using UnityEngine;
using UnityEngine.UI;

public class PlatformAlerts : MonoBehaviour
{

    private float playerYPosition;
    private PlayerMovement player;

    [SerializeField] private float startDetectionPosOffset = -18f;
    [SerializeField] private float detectionDistance = 30f;

    [SerializeField] private GameObject platformAlertsGO = null;
    public Image[] platformAlerts;
    private int platformLayerMask;

    [SerializeField] private Color alertInitialColor = Color.black;
    [SerializeField] private Color alertFinalColor = Color.black;

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

        for(int i = 0; i < platformAlerts.Length; i++)
        {
            Vector2 raycastStartPos = new Vector2(i - (platformAlerts.Length / 2f) + 0.5f, playerYPosition + startDetectionPosOffset);
            RaycastHit2D hit = Physics2D.Raycast(raycastStartPos, Vector2.down, detectionDistance, platformLayerMask);
            Debug.DrawRay(raycastStartPos, Vector2.down * detectionDistance, Color.red);

            if(hit)
            {
                //Debug.Log("Collision position x : " + hit.point.x);
                ShowAlert(i);
            } 
            else if(platformAlerts[i].enabled)
            {                
                HideAlert(i);
            }

            if(platformAlerts[i].enabled)
            {
                float distanceFromStartPos = Mathf.Abs(hit.point.y - raycastStartPos.y);
                UpdatePlatformAlertUI(i, distanceFromStartPos, detectionDistance);
            }
            
        }
    }

    private void ShowAlert(int alertIndex)
    {
        platformAlerts[alertIndex].enabled = true;
    }

    private void HideAlert(int alertIndex)
    {
        platformAlerts[alertIndex].enabled = false;
    }

    private void UpdatePlatformAlertUI(int alertIndex, float distance, float maxDistance)
    {
        //Debug.Log("Alert index : " + alertIndex + " ; Distance : " + distance);
        float colorRatio = distance / maxDistance;
        //Debug.Log(colorRatio);
        platformAlerts[alertIndex].color = Color.Lerp(alertFinalColor, alertInitialColor, colorRatio);        
    }

}
