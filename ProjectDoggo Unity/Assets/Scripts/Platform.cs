using UnityEngine;

public class Platform : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerDeath.instance.KillPlayer();
        }
    }
}
