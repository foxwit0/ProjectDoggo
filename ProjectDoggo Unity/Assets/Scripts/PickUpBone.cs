using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBone : MonoBehaviour
{
    
    public int boneID ; // Pour différencier un os d'un autre.


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            
            Inventory.instance.pushBoneInStack(boneID) ;
            Destroy(gameObject) ;
        }
    }
}
