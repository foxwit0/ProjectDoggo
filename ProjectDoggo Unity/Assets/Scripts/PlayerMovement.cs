using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float fallSpeed;
    public float horizontalMoveSpeed;
    public float speedRatio;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public BoxCollider2D boxCollider;

    private float verticalMovement;
    private float horizontalMovement;
    private Vector3 velocity = Vector3.zero;

    private void Awake() 
    {
        if(instance != null) 
        {
            Debug.LogError("Il y a plus d'une instance de PlayerMovement en jeu") ;
            return;
        }
        instance = this;
    }

    private void Start() 
    {
        rb = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        float modifierSpeed = 1f;
        horizontalMovement = Input.GetAxis("Horizontal") * horizontalMoveSpeed * Time.fixedDeltaTime; // Le fixedDeltaTime est nécessaire pour synchroniser le mouvement avec le FixedUpdate. Attention, comme il y a un input et de la physique, il est nécessaire de laisser cette partie dans Update.
        

        if(Input.GetAxis("Vertical") > 0.1f) // Le joueur ralentit
        {
            modifierSpeed *= 1f - speedRatio;
        } 
        else if (Input.GetAxis("Vertical") < -0.1f) //Le joueur accélère
        {
            modifierSpeed *= 1f + speedRatio;
        } 

        verticalMovement = -1f * modifierSpeed * fallSpeed * Time.fixedDeltaTime; // Le -1 permet d'avoir un mouvement vers le bas
        
    }

    private void FixedUpdate() 
    {
        MovePlayer(horizontalMovement, verticalMovement); // Calcule la position du joueur en permanence, même quand les fps sont bas.
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement,_verticalMovement);
        rb.velocity = Vector3.SmoothDamp(rb.velocity,targetVelocity, ref velocity, 0.05f); // Fait graduellement passer la vitesse du joueur de la précédente à la nouvelle.
    }
}
