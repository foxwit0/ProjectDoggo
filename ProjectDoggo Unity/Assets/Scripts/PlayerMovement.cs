using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float fallSpeedBasic;
    public float fallSpeedHigh;
    public float fallSpeedLow;
    private float fallSpeed;
    public float horizontalMoveSpeed;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public BoxCollider2D boxCollider;

    private float verticalMovement;
    private float horizontalMovement;
    private Vector3 velocity = Vector3.zero;


    //Debug
    [Tooltip("Can be enabled to allow the player to move freely")]
    [SerializeField] private bool debug = false;
    [SerializeField] private float debugMoveSpeed = 700f;

    #region Singleton
    private void Awake() 
    {
        if(instance != null) 
        {
            Debug.LogError("Il y a plus d'une instance de PlayerMovement en jeu") ;
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


    private void Update()
    {        

        horizontalMovement = Input.GetAxis("Horizontal") * horizontalMoveSpeed; // Le fixedDeltaTime est nécessaire pour synchroniser le mouvement avec le FixedUpdate. Attention, comme il y a un input et de la physique, il est nécessaire de laisser cette partie dans Update.
        

        #region VerticalSpeedCalculation
        if(Input.GetAxis("Vertical") > 0.1f) // Le joueur ralentit
        {
            fallSpeed = fallSpeedLow;
        } 
        else if (Input.GetAxis("Vertical") < -0.1f) //Le joueur accélère
        {
            fallSpeed = fallSpeedHigh;
        } 
        else
            fallSpeed = fallSpeedBasic;

        verticalMovement = -1 * fallSpeed; // Le -1 permet d'avoir un mouvement vers le bas
        #endregion

        #region Debug
        if(debug)
        {
            verticalMovement = Input.GetAxis("Vertical") * debugMoveSpeed;
        }
        #endregion
    }

    private void FixedUpdate() 
    {
        MovePlayer(horizontalMovement, verticalMovement); // Calcule la position du joueur en permanence, même quand les fps sont bas.
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);

        rb.velocity = targetVelocity * Time.fixedDeltaTime;
    }
}
