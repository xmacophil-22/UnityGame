using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicShop : MonoBehaviour
{
    private static Color grey = new Color(0.38f,0.38f,1f);
    private static Color blue = new Color(0.52f,0.52f,1f);
    private int selected;
    private PlayerStats playerStats;

    [SerializeField] private List<Image> slots;
    [SerializeField] private int[,] costs;
    private PlayerMovement playerMovement;
    private connection connection;
    //////////////////////////////////////////Shop initialisieren mit Kosten etc.
    private void Start(){
        connection = GameObject.Find("Connection").GetComponent<connection>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        costs = new int[4,3];
        costs[0,0] = 8;
        costs[1,0] = 6;
        costs[2,0] = 5;
        costs[3,0] = 4;
        costs[0,1] = 7;
        costs[1,1] = 6;
        costs[2,1] = 5;
        costs[3,1] = 4;
        costs[0,2] = 6;
        costs[1,2] = 5;
        costs[2,2] = 4;
        costs[3,2] = 3;
        
        selected = 0;
        slots[selected].color = grey;
        for (int i = 0; i < slots.Count; i++)
        {
            setLevel(i);
            setCosts(i);
        }
    }
    ///////////////////////////////////////erhöhen des Levels einer bestimmten Waffe i
    private void setLevel(int i){
        slots[i].gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = playerStats.getLevelOfMagic(i).ToString();
    }
    
    ///////////////////////////////////////errechnen der Kosten eines Upgrades der bestimmten Waffe i
    private void setCosts(int i){
        int multiPly = playerStats.getLevelOfMagic(i) + 1;
        for(int x = 0; x < 3; x++){
            slots[i].gameObject.transform.GetChild(3 + 2*x).GetComponent<TMPro.TextMeshProUGUI>().text = (costs[i,x] * multiPly).ToString();
        }
    }
    /////////////////////////////////////////Keyevents abfangen
    private void Update(){
        if(connection.lA){
            connection.lA = false;
            moveLeft();
        }
        if(connection.rA){
            connection.rA = false;
            moveRight();
        }
        if(connection.ePressed){
            connection.ePressed = false;
            upgrade();
        }
        if(connection.close){
            connection.close = false;
            close();
        }
    }
    /////////////////////////////////////////Keyevents verarbeiten, links, rechts, kaufen und schließen im Shop

    private void close(){
        playerMovement.enabled = true;
        GameObject.Find("RequestHandler").GetComponent<RequestHandler>().Upload();
        gameObject.SetActive(false);
    }
    private void upgrade(){
        int multiPly = playerStats.getLevelOfMagic(selected) + 1;
        if(playerStats.getAmountOfMaterial("charcoal") >= costs[selected,0] * multiPly&& playerStats.getAmountOfMaterial("whool") >= costs[selected,1] * multiPly && playerStats.getAmountOfMaterial("coin") >= costs[selected,2]  * multiPly){
            playerStats.changeAmountOfMaterial("charcoal", -1*costs[selected,0]);
            playerStats.changeAmountOfMaterial("whool", -1*costs[selected,1]);
            playerStats.changeAmountOfMaterial("coin", -1*costs[selected,2]);
            playerStats.increaseLevelOfMagic(selected);
            setLevel(selected);
            setCosts(selected);
        }
    }

    private void moveLeft(){
        slots[selected].color = blue;
        selected--;
        if(selected < 0){
            selected = 3;
        }
        slots[selected].color = grey;
    }

    private void moveRight(){
        slots[selected].color = blue;
        selected++;
        if(selected > 3){
            selected = 0;
        }
        slots[selected].color = grey;
    }
}
