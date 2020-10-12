using UnityEngine;

public class PlasmaCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll) 
    {
        //Collision avec le joueur
        if(coll.gameObject.CompareTag("Player"))
        {
            PlayerDeath.instance.KillPlayer();
            return;
        }
    }
}
