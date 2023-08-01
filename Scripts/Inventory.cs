using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Image> inventorySlots;
    [SerializeField] public List<GameObject> inventoryItems;
    public int currentInvent  {get; private set;} = 0;
    private static Color colorSelected = new Color(0.67f,0.67f,0.67f);
    private static Color colorStandart = new Color(1f,1f,1f);

    public List<float> timeMulti;
    
    //////////////////////////////////////initialisiere Inventar, mit ausgewähltem Item
    private void Awake(){
        for (int i = 0; i < 4; i++)
        {
            timeMulti.Add(1f);
        }
        inventorySlots[currentInvent].color = colorSelected;
        inventoryItems[currentInvent].SetActive(true);
    }

    ///////////////////////////////////////////wechselt Itemslot durch Tastatur
    private void Update(){
        if(Input.inputString != ""){
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number > 0 && number < 5){
                inventorySlots[currentInvent].color = colorStandart;
                inventoryItems[currentInvent].SetActive(false);
                currentInvent = number -1;
                inventorySlots[currentInvent].color = colorSelected;
                inventoryItems[currentInvent].SetActive(true);
            }
        }
    }
    public void setOne(){
        resetSelected();
        currentInvent = 0;
        setCurrent();
    }
    public void setTwo(){
        resetSelected();
        currentInvent = 1;
        setCurrent();
    }
    public void setThree(){
        resetSelected();
        currentInvent = 2;
        setCurrent();
    }
    public void setFour(){
        resetSelected();
        currentInvent = 3;
        setCurrent();
    }
    private void resetSelected(){
        inventorySlots[currentInvent].color = colorStandart;
        inventoryItems[currentInvent].SetActive(false);
    }

    private void setCurrent(){
        inventorySlots[currentInvent].color = colorSelected;
        inventoryItems[currentInvent].SetActive(true);
    }
}
