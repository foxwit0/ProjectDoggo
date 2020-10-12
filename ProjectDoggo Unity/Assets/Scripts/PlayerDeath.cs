using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public static PlayerDeath instance;

    private void Awake()
    {
        if(instance != null){
            Debug.LogWarning("Il y a plus d'une instance de PlayerDeath dans la scène.");
            return;
        }
        instance = this;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            KillPlayer();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    public void KillPlayer()
    {
        Debug.Log("Ooops, Player's dead...");

        // Bloquer les mouvements du personnage
        PlayerMovement.instance.enabled=false;
        PlayerMovement.instance.rb.velocity=Vector3.zero;

        // Bloquer les interactions
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.boxCollider.enabled=false;

        // PROVISOIRE - Désactivation du sprite renderer
        PlayerMovement.instance.spriteRenderer.enabled=false;

        // TODO - Jouer l'animation de mort

        // TODO - Afficher l'écran de game over

    }

    public void RespawnPlayer()
    {
        Debug.Log("Player's back baby!");

        // Débloquer les mouvements du personnage
        PlayerMovement.instance.enabled=true;

        // Débloquer les interactions
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.boxCollider.enabled=true;

        // PROVISOIRE - Réactivation du sprite renderer
        PlayerMovement.instance.spriteRenderer.enabled=true;

        // TODO - Animator et respawn ?

    }

    private void OnCollisionEnter2D(Collision2D coll) 
    {
        //Collision avec le plasma
        if(coll.gameObject.CompareTag("Plasma"))
        {
            PlayerDeath.instance.KillPlayer();
        }
    }

// Améliorations possibles : déplacer le spriteRenderer et le boxCollider ici ? Ou garder la logique de "toute la physique du personnage est à garder au même endroit" ? 

}
