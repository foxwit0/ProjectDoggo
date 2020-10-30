using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float timeOffset = 0f;
    [Header("Offset Y")]
    [SerializeField] private float yOffset = -7;
    [SerializeField] private float minYOffset = -6;
    [SerializeField] private float maxYOffset = -9;
    [Space]
    [SerializeField] private float yOffsetSpeed = 0.1f;
    [Header("Position X")]
    [SerializeField] private float posXSpeed = 0.1f;
    [Header("Size")]
    [SerializeField] private float size = 10f;
    [SerializeField] private float minSize = 9f;
    [SerializeField] private float maxSize = 12f;
    [Space]
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float dezoomSpeed = 0.1f;

    private Vector3 velocity;

    private Camera cam;
    private Vector3 targetPosition;
    private float targetPosX;
    private float targetPosXSpeed;
    private Vector3 posOffset;
    private float targetYOffset;
    private float targetYOffsetSpeed;
    private float targetSize;
    private float targetZoomSpeed;
    
    private void Start()
    {
        cam = transform.GetComponent<Camera>();
        targetPosition = Vector3.zero;
        targetPosX = 0f;
        targetPosXSpeed = 0f;
        posOffset = new Vector3(0,yOffset,-10);
        targetYOffset = yOffset;
        targetYOffsetSpeed = 0f;
        targetSize = size;
        targetZoomSpeed = 0f;
    }

    private void Update()
    {
        //----------Zoom/dezoom----------
        //Definition de targetPosOffset en fonction des input du joueur
        //Definition de targetSize en fonction des input du joueur
        if(Input.GetAxis("Vertical") > 0.1f) // Le joueur ralentit
        {
            targetYOffset = minYOffset;
            targetSize = minSize;
            targetPosX = player.transform.position.x / 2; //Permet du suivre le joueur sur l'axe X quand il ralentit et que la caméra zoom
        } 
        else if (Input.GetAxis("Vertical") < -0.1f) //Le joueur accélère
        {
            targetYOffset = maxYOffset;
            targetSize = maxSize;
            targetPosX = 0f;
        }
        else
        {
            targetYOffset = yOffset;
            targetSize = size;
            targetPosX = 0f;
        }

        //----------Les Rampes !----------
        //Définition de targetZoomSpeed en fonction du delta entre la size actuel et targetSize
        if(targetSize > cam.orthographicSize + dezoomSpeed) //Ici sur la comparasion entre le target et le réel on intègre dans la comparaison la speed qui devrait être appliqué
        {                                                   //pour éviter un effet de bagottement
            targetZoomSpeed = dezoomSpeed;
        }
        else if(targetSize < cam.orthographicSize - zoomSpeed)  //Même chose ici
        {
            targetZoomSpeed = -zoomSpeed;       //ici pour le zoom on met une valeur négative pour diminuer la valeur de size (et ainsi zoomer)
        }
        else
        {
            targetZoomSpeed = 0f;   //ici le zoom de bouge plus (il a atteint sa cible)
        }
        //Définition de targetYOffsetSpeed en fonction du delta entre le posOffset actuel et targetYOffset
        if(targetYOffset > posOffset.y + yOffsetSpeed)
        {
            targetYOffsetSpeed = yOffsetSpeed;
        }
        else if(targetYOffset < posOffset.y - yOffsetSpeed)
        {
            targetYOffsetSpeed = -yOffsetSpeed;
        }
        else
        {
            targetYOffsetSpeed = 0f;
        }
        //Définition de targetPosXSpeed en fonction du delta entre la posX réel actuel et targetPosX
        if(targetPosX > targetPosition.x + posXSpeed)
        {
            targetPosXSpeed = posXSpeed;
        }
        else if(targetPosX < targetPosition.x - posXSpeed)
        {
            targetPosXSpeed = -posXSpeed;
        }
        else
        {
            targetPosXSpeed = 0f;
            targetPosition = new Vector3(targetPosX, player.transform.position.y, 0);  // ça c'est pas très bien - a revoir !
        }

        //----------Mouvement de la caméra suivant le personnage----------
        //targetPosition = new Vector3(targetPosX, player.transform.position.y, 0); //Calcul de la position pour ne bouger que sur l'axe y du joueur, et pas l'axe x (sauf exeption)
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + posOffset, ref velocity, timeOffset);

    }

    private void FixedUpdate()
    {
        //----------Zoom/dezoom----------
        //Modification progressive de la size  / Indépendant du framerate
        cam.orthographicSize = cam.orthographicSize + targetZoomSpeed;

        //Modification progressive du posOffset / Indépendant du framerate
        posOffset = new Vector3(0,posOffset.y + targetYOffsetSpeed,-10);
        
        //Modification progressive de targetPosition / Indépendant du framerate
        targetPosition = new Vector3(targetPosition.x + targetPosXSpeed, player.transform.position.y, 0);
    }


}
