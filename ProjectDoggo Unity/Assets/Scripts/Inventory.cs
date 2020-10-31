using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance ;
    private Stack<int> boneStackLevel = new Stack<int>() ; // Pile pour le stockage des ID des os du niveau
    private Stack<int> boneStackBetweenCPs = new Stack<int>() ; // Pile pour le stockage des ID des os entre les checkpoints
    

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a déjà une instance d'Inventory dans la scène.");
        }

        instance = this ;
    }

    public void pushBoneInStack(int _boneID)
    {
        boneStackBetweenCPs.Push(_boneID) ;
    }

    public void RegisterBonesAtCheckpoint()
    {
        while(boneStackBetweenCPs.Count > 0)
        {
            boneStackLevel.Push(boneStackBetweenCPs.Pop());
        }
    }

    public Stack<int> GetBoneStackBetweenCPs()
    {
        return boneStackBetweenCPs;
    } 

    public void registerBonesWhenLevelEnds()
    {

        /*TODO : une fois qu'une méthode d'enregistrement des os aura été décidée, appeler cette méthode à la fin du niveau pour
        faire un pop de la pile provisoire selon la méthode d'enregistrement (en utilisant les Bone_ID comme ID matrices ?).*/

    }
    

}
