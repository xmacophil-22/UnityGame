using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collactables : MonoBehaviour
{
    [SerializeField] private string dropMaterial;
    [SerializeField] private int amount;
    [SerializeField] private int live;
    [SerializeField] private int pickupSlot;
    private GameObject myBar;
    private Animator animator;
    private PlayerMovement player;

    private Standarts standarts;
    private Inventory inventory;

    private PlayerStats playerStats;

    //////////////////////////////////////////Initialisierung
    private void Awake(){
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        standarts = GameObject.Find("GameManager").GetComponent<Standarts>();
        myBar = gameObject.transform.GetChild(0).gameObject;
        animator = myBar.GetComponent<Animator>();
    }
    
    public string getDropMaterial(){
        return dropMaterial;
    }
    ////////////////////////////////////////sammelt Item nach Ablauf des Balkens auf
    public IEnumerator collect(System.Action<bool> callback){
        if(inventory.currentInvent == pickupSlot){
        yield return StartCoroutine(showBar());
        standarts.StartCoroutine(standarts.changeAmountOfMaterial(dropMaterial, amount * (playerStats.getLevelOfMagic(pickupSlot) +1)));
        live--;
        if(live <= 0){
            callback(true);
        }
        else{
            callback(false);
        }
        }
    }
    ///////////////////////////////////////////////startet Ladebalken
    private IEnumerator showBar(){
        player.enabled = false;
        float sec = 4 * (inventory.timeMulti[pickupSlot]/(playerStats.getWeaponUpgrade(pickupSlot)+1));
        Debug.Log(sec);
        Debug.Log(inventory.timeMulti[pickupSlot]);
        Debug.Log(pickupSlot);
        Debug.Log(playerStats.getWeaponUpgrade(pickupSlot));
        animator.speed = 1/(sec);
        myBar.SetActive(true);
        yield return new WaitForSeconds(sec);
        myBar.SetActive(false);
        player.enabled = true;
    }
}
