using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject plasma = null;

    [Tooltip("Minimum distance between Plasma and Player on respawn")]
    [SerializeField] private float minDistanceFromPlasma = 10f;

    private Vector3 playerRespawnPosition;
    private Vector3 plasmaRespawnPosition;

    #region Singleton
    public static RespawnSystem instance;

    private void Awake() 
    {
        if(instance != null) 
        {
            Debug.LogError("Il y a plus d'une instance de RespawnSystem en jeu") ;
            return;
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        //Initialisation de la position de respawn au chargement de la scène
        playerRespawnPosition = PlayerMovement.instance.transform.position;

        if(plasma != null)
            plasmaRespawnPosition = plasma.transform.position;
        else
            Debug.LogWarning("Missing plasma object in inspector");
    }

    //Mise à jour de la position de respawn du joueur et du plasma à la prise d'un checkpoint
    public void UpdateRespawnData(Vector3 playerPos, Vector3 plasmaPos)
    {
        playerRespawnPosition = playerPos;
        plasmaRespawnPosition = plasmaPos;

        //Adapte la position de respawn du plasma si elle est trop proche de celle du joueur
        float distanceFromPlasma = Mathf.Abs(playerRespawnPosition.y - plasmaRespawnPosition.y);
        if(distanceFromPlasma < minDistanceFromPlasma)
            plasmaRespawnPosition.y = playerRespawnPosition.y + minDistanceFromPlasma;
    }

    public void Respawn()
    {
        //Respawn du joueur
        PlayerDeath.instance.RespawnPlayer();
        PlayerMovement.instance.transform.position = playerRespawnPosition;

        //Respawn du plasma
        plasma.transform.position = plasmaRespawnPosition;

        //Réactivation du temps
        Time.timeScale = 1;
    }
}