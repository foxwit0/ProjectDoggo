using UnityEngine;

public class PlatformAlerts : MonoBehaviour
{

    private float playerYPosition;
    private PlayerMovement player;

    [SerializeField] private float startDetectionPosOffset = -18f;
    [SerializeField] private float detectionDistance = 30f;

    void Start()
    {
        player = PlayerMovement.instance;
    }

    void Update()
    {
        playerYPosition = player.transform.position.y;        

        for(int i = 0; i <= 16; i++)
        {
            Vector2 raycastStartPos = new Vector2(i - 8f, playerYPosition + startDetectionPosOffset);
            RaycastHit2D hit = Physics2D.Raycast(raycastStartPos, Vector2.down, detectionDistance);

            if(hit)
            {
                if(hit.collider.CompareTag("Platform"))
                {
                    //Debug.Log("Collision position x : " + hit.point.x);
                }
            } 

        }
    }
}
