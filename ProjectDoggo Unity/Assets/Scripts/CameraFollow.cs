using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Follow")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private float timeOffset = 0f;
    [Space]
    [Header("Zoom Feature")]
    [SerializeField] private bool zoomActivated = true;
    [Header("Size")]
    [SerializeField] private float size = 10f;
    [SerializeField] private float minSize = 9f;
    [SerializeField] private float maxSize = 12f;
    [SerializeField] private float zoomSpeed = 0.1f;
    [Header("Offset Y")]
    [SerializeField] private float yOffset = -7;
    [SerializeField] private float minYOffset = -6;
    [SerializeField] private float maxYOffset = -9;
    [SerializeField] private float yOffsetSpeed = 0.1f;
    [Header("Position X")]
    [SerializeField] private float xPositionSpeed = 0.1f;

    private Vector3 velocity;

    private Camera cam;
    private float targetSize;
    private float yTargetOff;
    private Vector3 lerpPosition;
    private float xLerpPos;
    private float xTargetPos;
    private float yLerpOff;
    
    private void Start()
    {
        cam = transform.GetComponent<Camera>();
        targetSize = size;
        yTargetOff = yOffset;
        yLerpOff = yOffset;
        xTargetPos = 0f;
        xLerpPos = 0f;
        lerpPosition = new Vector3(xLerpPos, player.transform.position.y + yLerpOff, -10);
    }

    private void Update()
    {
        //----------Zoom/dezoom----------
        if(zoomActivated)
        {
            //Definition de targetSize, yTargetOff et xTargetPos en fonction des input du joueur
            if(Input.GetAxis("Vertical") > 0.1f) // Le joueur ralentit
            {
                targetSize = minSize;
                yTargetOff = minYOffset;
                xTargetPos = player.transform.position.x / 2; //Permet du suivre le joueur sur l'axe X quand il ralentit et que la caméra zoom
            } 
            else if (Input.GetAxis("Vertical") < -0.1f) //Le joueur accélère
            {
                targetSize = maxSize;
                yTargetOff = maxYOffset;
                xTargetPos = 0f;
            }
            else
            {
                targetSize = size;
                yTargetOff = yOffset;
                xTargetPos = 0f;
            }
        }

        //----------Mouvement de la caméra suivant le personnage----------
        //Création d'un vecteur interpolé sur la position X et l'offset Y depuis la position Y du joueur
        lerpPosition = new Vector3(xLerpPos, player.transform.position.y + yLerpOff, -10);
        //Léger smooth sur le lerpPosition et écriture final de la position de la caméra
        transform.position = Vector3.SmoothDamp(transform.position, lerpPosition, ref velocity, timeOffset);

    }

    private void FixedUpdate()
    {
        //----------Zoom/dezoom----------
        //Modification progressive de la size  / Indépendant du framerate
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomSpeed);

        //Modification progressive du yLerpOff / Indépendant du framerate
        yLerpOff = Mathf.Lerp(yLerpOff, yTargetOff, yOffsetSpeed);

        //Modification progressive de xLerpPos / Indépendant du framerate
        xLerpPos = Mathf.Lerp(xLerpPos, xTargetPos, xPositionSpeed);

    }

}
