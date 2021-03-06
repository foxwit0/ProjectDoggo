﻿using UnityEngine;
using System.Collections.Generic;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject plasma = null;
    private PlatformAlerts platformAlerts;
    [SerializeField] private GameObject bonesGO = null;
    private List<Bone> bones = new List<Bone>();

    [Tooltip("Minimum distance between Plasma and Player on respawn")]
    [SerializeField] private float minDistanceFromPlasma = 10f;

    private Vector3 playerRespawnPosition;
    private Vector3 plasmaRespawnPosition;

    #region Singleton
    public static RespawnSystem instance;

    private void Awake() 
    {
        if(instance != null) 
        {
            Debug.LogError("Il y a plus d'une instance de RespawnSystem en jeu") ;
            return;
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        //Initialisation de la position de respawn au chargement de la scène
        playerRespawnPosition = PlayerMovement.instance.transform.position;

        if(plasma != null)
            plasmaRespawnPosition = plasma.transform.position;
        else
            Debug.LogWarning("Missing plasma object in inspector");

        //Récupératin du GO PlatformAlerts
        platformAlerts = gameObject.GetComponent<PlatformAlerts>();

        //Récupération des GO Bone
        foreach(Bone bone in bonesGO.GetComponentsInChildren<Bone>())
        {
            bones.Add(bone);
        }
    }

    //Mise à jour de la position de respawn du joueur et du plasma à la prise d'un checkpoint
    public void UpdateRespawnData(Vector3 playerPos, Vector3 plasmaPos)
    {
        playerRespawnPosition = playerPos;
        plasmaRespawnPosition = plasmaPos;

        //Adapte la position de respawn du plasma si elle est trop proche de celle du joueur
        float distanceFromPlasma = Mathf.Abs(playerRespawnPosition.y - plasmaRespawnPosition.y);
        if(distanceFromPlasma < minDistanceFromPlasma)
            plasmaRespawnPosition.y = playerRespawnPosition.y + minDistanceFromPlasma;
    }

    public void Respawn()
    {
        //Respawn du joueur
        PlayerDeath.instance.RespawnPlayer();
        PlayerMovement.instance.transform.position = playerRespawnPosition;

        //Respawn du plasma
        plasma.transform.position = plasmaRespawnPosition;

        //Désactivation des alertes plateforme en cours
        platformAlerts.HideAllAlerts();

        //Réactivation des os récupérés depuis le dernier checkpoint
        Inventory.instance.RestoreBonesOnRespawn(bones);

        //Réactivation du temps
        Time.timeScale = 1;
    }
}