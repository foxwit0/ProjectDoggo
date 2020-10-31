using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public static PlayerDeath instance;
    public GameObject DeathMenu;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerDeath dans la scène.");
            return;
        }
        instance = this;
    }
    
    void Update()
    {
        // Provisoire pour tests de la mort.
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
        // Bloquer les mouvements du personnage
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.rb.velocity = Vector3.zero;

        // Bloquer les interactions
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.boxCollider.enabled = false;

        // PROVISOIRE - Désactivation du sprite renderer
        PlayerMovement.instance.spriteRenderer.enabled = false;

        // TODO - Jouer l'animation de mort

        //Afficher l'écran de mort
        DeathMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RespawnPlayer()
    {
        // Débloquer les mouvements du personnage
        PlayerMovement.instance.enabled = true;

        // Débloquer les interactions
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.boxCollider.enabled = true;

        // PROVISOIRE - Réactivation du sprite renderer
        PlayerMovement.instance.spriteRenderer.enabled = true;

        // TODO - Animator et respawn ?

    }

// Améliorations possibles : déplacer le spriteRenderer et le boxCollider ici ? Ou garder la logique de "toute la physique du personnage est à garder au même endroit" ? 

}
