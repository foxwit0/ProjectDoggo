using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float levelHeight;   

    #region Singleton
    public static LevelManager instance;

    void Awake()
    {
        if(instance!= null)
        {
            Debug.LogError("Il y a plus d'une instance de LevelManager en jeu") ;
            return;
        }
        instance = this;
    }
    #endregion Singleton
}
