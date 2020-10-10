using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField] private GameObject player;
    //[SerializeField] private float timeOffset;
    //[SerializeField] private Vector3 posOffset;

    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(0f, player.transform.position.y, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + posOffset, ref velocity, timeOffset);
    }
}
