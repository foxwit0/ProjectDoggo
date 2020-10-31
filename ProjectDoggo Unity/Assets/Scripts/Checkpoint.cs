using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private RespawnSystem respawnSystem = null;
    [SerializeField] private GameObject plasma = null;

    void Start() 
    {
        if(respawnSystem == null)
            Debug.LogWarning("Missing RespawnSystem object in inspector of Checkpoint.cs");
        if(plasma == null)
            Debug.LogWarning("Missing plasma object in inspector of Checkpoint.cs");
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        //Si le joueur entre en collision avec le checkpoint
        if(other.CompareTag("Player"))
        {
            respawnSystem.UpdateRespawnData(PlayerMovement.instance.transform.position, plasma.transform.position);
            Destroy(gameObject);
        }
    }

}
