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

    void Start()
    {
        player = PlayerMovement.instance;

        //Récupération des GO de chaque alerte plateforme
        platformAlerts = platformAlertsGO.GetComponentsInChildren<Image>();
    }

    void Update()
    {
        playerYPosition = player.transform.position.y;        

        for(int i = 0; i < platformAlerts.Length; i++)
        {
            Vector2 raycastStartPos = new Vector2(i - (platformAlerts.Length / 2f) + 0.5f, playerYPosition + startDetectionPosOffset);
            RaycastHit2D hit = Physics2D.Raycast(raycastStartPos, Vector2.down, detectionDistance);
            Debug.DrawRay(raycastStartPos, Vector2.down * detectionDistance, Color.red);

            if(hit)
            {
                if(hit.collider.CompareTag("Platform"))
                {
                    //Debug.Log("Collision position x : " + hit.point.x);
                    ShowAlert(i);
                }
                else
                {
                    if(platformAlerts[i].enabled)
                        HideAlert(i);
                }
            } 
            else
            {
                if(platformAlerts[i].enabled)
                    HideAlert(i);
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

}
