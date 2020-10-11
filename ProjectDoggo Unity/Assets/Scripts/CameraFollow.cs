using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float timeOffset = 0f;
    [SerializeField] private Vector3 posOffset = Vector3.zero;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPositionY = new Vector3(0f, player.transform.position.y, 0); //Calcul de la position pour ne bouger que sur l'axe y du joueur, et pas l'axe x

        transform.position = Vector3.SmoothDamp(transform.position, playerPositionY + posOffset, ref velocity, timeOffset);
    }
}
