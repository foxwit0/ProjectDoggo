using UnityEngine;

public class Bone : MonoBehaviour
{
    
    public int boneID ; // Pour différencier un os d'un autre.

    private SpriteRenderer sprite;
    private ParticleSystem particles;
    private CircleCollider2D myCollider;

    void Start()
    {
        sprite = transform.GetComponent<SpriteRenderer>();
        particles = transform.GetComponent<ParticleSystem>();
        myCollider = transform.GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.pushBoneInStack(boneID) ;
            Disable();            
        }
    }

    private void Disable()
    {
        //Désactivation de l'os
        sprite.enabled = false;
        myCollider.enabled = false;
        particles.Stop();
    }

    public void Enable()
    {
        //Activation de l'os
        sprite.enabled = true;
        myCollider.enabled = true;
        particles.Play();
    }
}
