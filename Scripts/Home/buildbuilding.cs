using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class buildbuilding : MonoBehaviour
{
    [SerializeField] private List<string> resourcesNes;
    [SerializeField] private List<int> resourcesNesAmount;
    private PlayerMovement player;
    private bool isActive = false;
    private bool touching = false;
    private SpriteRenderer spriteRenderer;
    private PlayerStats playerStats;
    [SerializeField] GameObject inventory;
    [SerializeField] int kind;
    private IDictionary<int, Action> kindStats;
    private IDictionary<int, bool> kindStatsGet;
    private connection connection;

    /////////////////////////////////////////init
    private void Awake(){
        connection = GameObject.Find("Connection").GetComponent<connection>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        kindStats = new Dictionary<int, Action>();
        kindStats.Add(0, playerStats.setMiddleHouse);
        kindStats.Add(1, playerStats.setArmorShop);
        kindStats.Add(2,playerStats.setMagicShop);
        kindStatsGet = new Dictionary<int, bool>();
        kindStatsGet.Add(0, playerStats.middleHouse);
        kindStatsGet.Add(1, playerStats.armorShop);
        kindStatsGet.Add(2,playerStats.magicShop);
        isActive = kindStatsGet[kind];
        if(isActive){
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }
    ////////////////////////////////////////////Wenn Gebäude berührt und E gedrückt, Gebäude bauen
    private void OnTriggerEnter2D(Collider2D collider2D){
        touching = true;
    }
    private void OnTriggerExit2D(Collider2D collider2D){
        touching = false;
    }

    private void Update(){
        if(touching){
            if(connection.ePressed){
            if(isActive){
                inventory.SetActive(true);
                player.enabled = false;
            }
            else{
                bool possible = true;
                for(int x = 0; x < resourcesNes.Count; x++)
                {
                    if(playerStats.getAmountOfMaterial(resourcesNes[x]) < resourcesNesAmount[x]){
                        possible = false;
                        break;
                    }
                }
                if(possible){
                   for(int x = 0; x < resourcesNes.Count; x++)
                    {
                        playerStats.changeAmountOfMaterial(resourcesNes[x], resourcesNesAmount[x] * -1);
                    } 
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                    kindStats[kind]();
                    isActive = true;
                }
            }
        }
        }
    }
}
