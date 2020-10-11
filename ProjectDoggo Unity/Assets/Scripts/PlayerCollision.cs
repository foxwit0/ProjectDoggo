using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll) 
    {
        //Collision avec une plateforme
        if(coll.gameObject.CompareTag("Platform"))
        {
            PlayerDeath.instance.KillPlayer();
        }
    }
}
