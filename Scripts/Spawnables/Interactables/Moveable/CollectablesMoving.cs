using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesMoving : MonoBehaviour
{
    [SerializeField] private string dropMaterial;
    [SerializeField] private int amount;
    [SerializeField] private int pickupSlot;
    private Inventory inventory;

    private Standarts standarts;
    private PlayerStats playerStats;
    ////////////////////////////////Initialisierung
    private void Awake(){
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        standarts = GameObject.Find("GameManager").GetComponent<Standarts>();
    }

    //////////////////////////////Sammelt Schaf auf ohne es zu zerstören
    public void collect(){
        if(inventory.currentInvent == pickupSlot){
        standarts.StartCoroutine(standarts.changeAmountOfMaterial(dropMaterial, amount * (playerStats.getLevelOfMagic(pickupSlot) +1)));
        amount = 0;
        }
    }

    public string getDropMaterial(){
        return dropMaterial;
    }
}
