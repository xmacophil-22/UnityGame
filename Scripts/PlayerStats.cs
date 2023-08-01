using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerStats : MonoBehaviour
{
    public bool middleHouse;
    public bool armorShop;
    public bool magicShop;

    public int[] weaponUpgrade;

    public bool endOfLevelHelperWood;
    public bool endOfLevelHelperStone;
    public bool endOfLevelHelperCharcoal;
    public bool endOfLevelHelperWhool;
    public bool endOfLevelHelperIron;
    public bool endOfLevelHelperCoin;

    public bool[] endOfLevelHelpers;
    public int[] levelOfHelpers;
    public int[] levelOfMagic;
    public string[] collactables = {"wood", "stone", "charcoal", "whool", "iron", "coin"};
    public long[] stats;

    public long highScore;

    //////////////////////////////////////////////Fortschritt des Spielers der Hochgeladen und wieder Heruntergeladen werden kann. Muss deshalb public sein :( aber keine Gefahr :)
    private void Awake(){
        highScore = 1;
        levelOfHelpers = new int[]{0,0,0,0,0,0};
        weaponUpgrade = new int[]{0,0,0,0};
        levelOfMagic = new int[]{0,0,0,0};
        endOfLevelHelpers = new bool[]{endOfLevelHelperWood, endOfLevelHelperStone, endOfLevelHelperCharcoal, endOfLevelHelperWhool, endOfLevelHelperIron, endOfLevelHelperCoin};
        int numberPlayers = GameObject.FindGameObjectsWithTag("PlayerStats").Length;
        if(numberPlayers > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(this.gameObject);
        }
    }
    //////////////////////////////////////////////Hochladen
    private void OnApplicationQuit(){
        RequestHandler requestHandler = GameObject.Find("RequestHandler").GetComponent<RequestHandler>();
        requestHandler.Upload();
        requestHandler.putScore();
    }


    /////////////////////////////////////////initialisieren, updaten und ändern von Stats
    private void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        armorShop = false;
        middleHouse = false;
        magicShop = false;
        stats = new long[]{0,0,0,0,0,0};
        GameObject.Find("Starter").GetComponent<Starter>().hostLogin();
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
    }

    public void addHelperStuff(){
        for (int i = 0; i < endOfLevelHelpers.Length; i++)
        {
            if(endOfLevelHelpers[i]){
                changeAmountOfMaterial(collactables[i], levelOfHelpers[i]);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        GameObject helpers = GameObject.Find("Helpers");
        if(helpers != null){
        for (int i = 0; i < endOfLevelHelpers.Length; i++)
        {
            helpers.transform.GetChild(i).gameObject.SetActive(endOfLevelHelpers[i]);
        }
        }
        
    }

    public void reload(){
        GameObject helpers = GameObject.Find("Helpers");
        if(helpers != null){
        for (int i = 0; i < endOfLevelHelpers.Length; i++)
        {
            helpers.transform.GetChild(i).gameObject.SetActive(endOfLevelHelpers[i]);
        }
        }
        if(armorShop){
            SpriteRenderer spriteRenderer = GameObject.Find("ArmorShop").GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
        if(magicShop){
            SpriteRenderer spriteRenderer = GameObject.Find("MagicShop").GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
        if(middleHouse){
            SpriteRenderer spriteRenderer = GameObject.Find("MiddleHouse").GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }
    public long getAmountOfMaterial(string material){
        return stats[Array.IndexOf(collactables, material)];
    }

    public void changeAmountOfMaterial(string material, long amount){
        stats[Array.IndexOf(collactables, material)] += amount;
        Debug.Log(material + ": " + stats[Array.IndexOf(collactables, material)]);
    }

    public void setMagicShop(){
        magicShop = true;
    }
    public void setMiddleHouse(){
        middleHouse = true;
    }
    public void setArmorShop(){
        armorShop = true;
    }

    public void setEndOfLevelHelper(int position){
        endOfLevelHelpers[position] = true;
    }

    public bool getEndOfLevelHelper(int position){
        return endOfLevelHelpers[position];
    }

    public int getWeaponUpgrade(int index){
        return weaponUpgrade[index];
    }

    public void increaseWeaponUpgrade(int index){
        weaponUpgrade[index]++;
    }

    public void increaseLevelOfHelper(int position){
        levelOfHelpers[position]++;
    }

    public int getLevelOfHelper(int position){
        return levelOfHelpers[position];
    }

    public void increaseLevelOfMagic(int index){
        levelOfMagic[index]++;
    }

    public int getLevelOfMagic(int index){
        return levelOfMagic[index];
    }

    public void increseHighScore(int amount){
        highScore += amount;
    }

    public long getHighScore(){
        return highScore;
    }

}
