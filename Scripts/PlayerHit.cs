using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Inventory inventory;
    private connection connection;
    private PlayerCollection playerCollection;
    private void Awake(){
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        connection = GameObject.Find("Connection").GetComponent<connection>();
        playerCollection = gameObject.GetComponent<PlayerCollection>();
    }

    private void FixedUpdate(){
        //// MOVMENT interact
        if(connection.ePressed){
            interact();
        }
    }

    /////////////////////////////////////Spieler interagiert je nach ausgewähltem Werkzeug
    public void interact(){
        GameObject item = inventory.inventoryItems[inventory.currentInvent];
            item.GetComponent<Animator>().SetTrigger("hit");
            playerCollection.interact();
            if(inventory.currentInvent == 0){
                item.GetComponent<Sword>().makeDamage();
                connection.ePressed = false;
            }
    }
}
