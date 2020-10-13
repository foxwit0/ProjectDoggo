using UnityEngine;

public class PlasmaMovement : MonoBehaviour
{   
    private float fallSpeed;
    
    [Tooltip("Fallspeed offset compared to player's fallspeed")]
    public float fallSpeedOffset = -100f;
    public float horizontalSpeed = 100f;
    public float maxHorizontalBalance = 2f;

    [HideInInspector] public Rigidbody2D rb;

    //Debug
    [Tooltip("Can be enabled to stop plasma's movement")]
    [SerializeField] private bool debugStopMovement = false;
    
    private void Start() 
    {
        rb = transform.GetComponent<Rigidbody2D>();

        fallSpeed = PlayerMovement.instance.fallSpeed + fallSpeedOffset;
    }

    private void FixedUpdate() 
    {
        if(rb.position.x >= maxHorizontalBalance) horizontalSpeed = -horizontalSpeed;
        if(rb.position.x <= -maxHorizontalBalance) horizontalSpeed = -horizontalSpeed;
        
        Vector3 targetVelocity = new Vector2(horizontalSpeed, -1f * fallSpeed); // Le -1 permet d'avoir un mouvement vers le bas

        rb.velocity = targetVelocity * Time.fixedDeltaTime;

        if(debugStopMovement)
        {
            rb.velocity = Vector3.zero;
        }
        
    }
}
