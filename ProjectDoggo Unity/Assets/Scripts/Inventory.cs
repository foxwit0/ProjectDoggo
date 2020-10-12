using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance ;
    private Stack<int> boneStackProvisoire = new Stack<int>() ; // Pile pour le stockage des ID des os du niveau ==> à modifier éventuellement en fonction de la gestion finale.
    

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
        boneStackProvisoire.Push(_boneID) ;
    }

    public void emptyBoneStack() // Requis à la mort du joueur.
    {
        while(boneStackProvisoire.Count>0)
        {
            boneStackProvisoire.Pop();
        }
    }

    public void registerBonesWhenLevelEnds()
    {

        /*TODO : une fois qu'une méthode d'enregistrement des os aura été décidée, appeler cette méthode à la fin du niveau pour
        faire un pop de la pile provisoire selon la méthode d'enregistrement (en utilisant les Bone_ID comme ID matrices ?).*/

    }
    

}
