using UnityEngine;

public class PlasmaMovement : MonoBehaviour
{   
    private float fallSpeed;

    [HideInInspector] public Rigidbody2D rb;

    private void Start() 
    {
        rb = transform.GetComponent<Rigidbody2D>();

        fallSpeed = PlayerMovement.instance.fallSpeed - 100f;
    }

    private void FixedUpdate() 
    {
        Vector3 targetVelocity = new Vector2(0f, -1f * fallSpeed); // Le -1 permet d'avoir un mouvement vers le bas

        rb.velocity = targetVelocity * Time.fixedDeltaTime;
    }
}
