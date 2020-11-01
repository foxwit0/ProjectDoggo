using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private RespawnSystem respawnSystem = null;
    [SerializeField] private GameObject plasma = null;

    private LineRenderer lineRenderer;
    [Header("Width Variation")]
    [SerializeField] float widthVariationPerUpdate = 0.005f;
    [SerializeField] float maxWidthVariation = 0.25f;
    private float startWidth;
    private bool widthExpanding = true;

    void Start() 
    {
        if(respawnSystem == null)
            Debug.LogWarning("Missing RespawnSystem object in inspector of Checkpoint.cs");
        if(plasma == null)
            Debug.LogWarning("Missing plasma object in inspector of Checkpoint.cs");

        lineRenderer = transform.GetComponent<LineRenderer>();
        startWidth = lineRenderer.startWidth;
    }

    void Update()
    {
        //Variation de l'épaisseur du checkpoint
        if(widthExpanding)
        {
            lineRenderer.startWidth += widthVariationPerUpdate;
            if(lineRenderer.startWidth >= startWidth + maxWidthVariation)
                widthExpanding = false;
        }
        else
        {
            lineRenderer.startWidth -= widthVariationPerUpdate;
            if(lineRenderer.startWidth <= startWidth)
                widthExpanding = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        //Si le joueur entre en collision avec le checkpoint
        if(other.CompareTag("Player"))
        {
            respawnSystem.UpdateRespawnData(PlayerMovement.instance.transform.position, plasma.transform.position);
            Inventory.instance.RegisterBonesAtCheckpoint();
            Destroy(gameObject);
        }
    }
}
