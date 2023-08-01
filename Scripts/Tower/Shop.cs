using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop : MonoBehaviour
{
    [SerializeField] private Tower tower;
    private PlayerStats playerStats;
    private RequestHandler requestHandler;
    private connection connection;
    private PlayerMovement playerMovement;

    ////////////////////////////////////////////////ähnlich wie andere Shops
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        connection = GameObject.Find("Connection").GetComponent<connection>();
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        requestHandler = GameObject.Find("RequestHandler").GetComponent<RequestHandler>();
        setLevel();
        setCosts();
    }


    private void setLevel(){
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerStats.getHighScore().ToString();
    }

    private void setCosts(){
        for (int i = 2; i < 8; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (playerStats.getHighScore()/10 +1).ToString();
        }
    }

    private void close(){
        tower.reload();
        Serialize.seri(playerStats);
        playerMovement.enabled = true;
        gameObject.SetActive(false);
    }

    //////////////////////////////////////////beim Kaufen, Score online Hochladen
    private void buy(){
        bool buyable = true;
        long costs = playerStats.getHighScore()/10 +1;
        foreach(string s in playerStats.collactables){
            if(playerStats.getAmountOfMaterial(s) < costs){
                buyable = false;
            }
        }
        if(buyable){
            foreach(string s in playerStats.collactables){
                playerStats.changeAmountOfMaterial(s, -1 * costs);
            }
            playerStats.increseHighScore(1);
            setCosts();
            setLevel();
            requestHandler.putScore();
        }
    }
    void Update()
    {
        if(connection.ePressed){
            connection.ePressed = false;
            buy();
        }
        if(connection.close){
            connection.close = false;
            close();
        }
    }
}
