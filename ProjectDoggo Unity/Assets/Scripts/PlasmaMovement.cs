using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaMovement : MonoBehaviour
{   
    public static PlasmaMovement instance;

    public float fallSpeed;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public BoxCollider2D boxCollider;

    #region Singleton
    private void Awake() 
    {
        if(instance != null) 
        {
            Debug.LogError("Il y a plus d'une instance de PlasmaMovement en jeu") ;
            return;
        }
        instance = this;
    }
    #endregion

    private void Start() 
    {
        rb = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() 
    {
        Vector3 targetVelocity = new Vector2(0f, -1f * fallSpeed); // Le -1 permet d'avoir un mouvement vers le bas

        rb.velocity = targetVelocity * Time.fixedDeltaTime;
    }
}
