using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;

    public float fallSpeed;
    public float horizontalMoveSpeed;

    public Rigidbody2D rb;

    private float verticalMovement;
    private float horizontalMovement;
    private Vector3 velocity = Vector3.zero;
    private float multiplierReducedSpeed=0.75f;
    private float multiplierTurboSpeed=1.5f;


    private void Awake() {
        if(instance!=null) {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement en jeu") ;
            return;
        }
        instance=this;
    }


    void Start()
    {
        
    }


    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal")*horizontalMoveSpeed*Time.fixedDeltaTime; // Le fixedDeltaTime est nécessaire pour synchroniser le mouvement avec le FixedUpdate. Attention, comme il y a un input et de la physique, il est nécessaire de laisser cette partie dans Update.
        
        if(Input.GetAxis("Vertical")>0.1) // Le joueur essaie de ralentir
        {
            verticalMovement = -multiplierReducedSpeed*fallSpeed*Time.fixedDeltaTime; // Fonctionnement sans gravité. Le - permet d'input une valeur absolue mais d'avoir le déplacement dans le bon sens.
        } else if (Input.GetAxis("Vertical")<-0.1) //Le joueur essaie d'accélérer
        {
            verticalMovement = -multiplierTurboSpeed*fallSpeed*Time.fixedDeltaTime; // Fonctionnement sans gravité. Le - permet d'input une valeur absolue mais d'avoir le déplacement dans le bon sens.
        } else {
            verticalMovement = -fallSpeed*Time.fixedDeltaTime; // Fonctionnement sans gravité. Le - permet d'input une valeur absolue mais d'avoir le déplacement dans le bon sens.
        }
        
    }

    private void FixedUpdate() {
        MovePlayer(horizontalMovement, verticalMovement); // Calcule la position du joueur en permanence, même quand les fps sont bas.
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement,_verticalMovement);
        rb.velocity = Vector3.SmoothDamp(rb.velocity,targetVelocity, ref velocity, 0.05f); // Fait graduellement passer la vitesse du joueur de la précédente à la nouvelle.
    }
}
