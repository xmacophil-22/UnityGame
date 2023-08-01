using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class middleHouse : MonoBehaviour
{
    private connection connection;
    private int current;
    private static Color green = new Color(0.7f,1f,0.7f);
    private static Color grey = new Color(0.8f,0.76f,0.76f);
    private static Color darkGreen = new Color(0.24f,0.35f,0.24f);
    [SerializeField] private Image[] helpers;
    [SerializeField] private int[] helpersCost;
    [SerializeField] public GameObject[] helpersSprites;
    private PlayerStats playerStats;
    private PlayerMovement playerMovement;

    //////////////////////////////////////////Shop initialisieren mit Kosten etc.
    private void Awake(){
        connection = GameObject.Find("Connection").GetComponent<connection>();
        current = 0;
        setColor(current, new Color(helpers[current].color.r-0.2f, helpers[current].color.g-0.2f, helpers[current].color.b-0.2f));
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        for (int i = 0; i < helpers.Length; i++)
        {
            setCosts(i);
            setLevel(i);
        
        }
    }
    /////////////////////////////////Kosten eines Upgrades setzen
    private void setCosts(int i){
        helpers[i].gameObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = helpersCost[i].ToString();
    }
    ///////////////////////////////Level eines Helfers setzen 
    private void setLevel(int i){
        helpers[i].gameObject.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = playerStats.getLevelOfHelper(i).ToString();
    }
    ////////////////////////////////Keyevents abfangen
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
            buy();
        }
        if(connection.close){
            connection.close = false;
            close();
        }
    }
    ////////////////////////////////Keyevents verarbeiten
    public void close(){
        for (int i = 0; i < 6; i++)
        {
            helpersSprites[i].SetActive(playerStats.getEndOfLevelHelper(i));
        }
        playerMovement.enabled = true;
        GameObject.Find("RequestHandler").GetComponent<RequestHandler>().Upload();
        gameObject.SetActive(false);
    }

    private void moveLeft(){
        setColor(current, new Color(helpers[current].color.r+0.2f, helpers[current].color.g+0.2f, helpers[current].color.b+0.2f));
        /*if(playerStats.getEndOfLevelHelper(current)){
            setColor(current, getChangeColor((0.45f/10)*playerStats.getLevelOfHelper(current)));
        }
        else{
            setColor(current, new Color(helpers[current].color.r+0.2f, helpers[current].color.g+0.2f, helpers[current].color.b+0.2f));
        }*/
        current--;
        if(current < 0){
            current = 5;
        }
        setColor(current, new Color(helpers[current].color.r-0.2f, helpers[current].color.g-0.2f, helpers[current].color.b-0.2f));
    }
    private void moveRight(){
        setColor(current, new Color(helpers[current].color.r+0.2f, helpers[current].color.g+0.2f, helpers[current].color.b+0.2f));
        /*if(playerStats.getEndOfLevelHelper(current)){
            setColor(current, getChangeColor((0.45f/10)*playerStats.getLevelOfHelper(current)));
        }
        else{
            setColor(current, new Color(helpers[current].color.r+0.2f, helpers[current].color.g+0.2f, helpers[current].color.b+0.2f));
        }*/
        current++;
        if(current > 5){
            current = 0;
        }
        
        setColor(current, new Color(helpers[current].color.r-0.2f, helpers[current].color.g-0.2f, helpers[current].color.b-0.2f));
    }

    private Color getChangeColor(float change){
        return new Color(green.r - change, green.g - change, green.b - change);
    }

    private void setColor(int index, Color color){
        helpers[index].color = color;
    }

    private void buy(){

        if(playerStats.getAmountOfMaterial("coin") >= helpersCost[current]){
            playerStats.changeAmountOfMaterial("coin", -1*helpersCost[current]);
            helpersCost[current] *= 2;
            setCosts(current);
            playerStats.setEndOfLevelHelper(current);
            playerStats.increaseLevelOfHelper(current);
            setLevel(current);
            //setColor(current, getChangeColor((0.45f/10)*playerStats.getLevelOfHelper(current)));
        }
    }

}
