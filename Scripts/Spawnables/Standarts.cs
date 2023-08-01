using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Standarts : MonoBehaviour
{
    ////////////////////////////////////////////Alle standard Variablen für Mapgeneration etc.
    [Header("Overworld")]
    [Header("Normal")]
    [SerializeField] private List<GameObject> plattformsN;
    [SerializeField] private List<GameObject> interactablesN1;
    [SerializeField] private List<GameObject> interactablesN2;
    private IDictionary<int, List<GameObject>> interactablesN;

    [Header("Forrest")]
    [SerializeField] private List<GameObject> plattformsF;
    [SerializeField] private List<GameObject> interactablesF1;
    [SerializeField] private List<GameObject> interactablesF2;

    [Header("Sky")]
    [SerializeField] private List<GameObject> plattformsS;
    [SerializeField] private List<GameObject> interactablesS1;
    [SerializeField] private List<GameObject> interactablesS2;

    [Header("Underground")]
    [SerializeField] private List<GameObject> plattformsU;
    [SerializeField] private List<GameObject> interactablesU1;
    [SerializeField] private List<GameObject> interactablesU2;

    [Header("Castle")]
    [SerializeField] private List<GameObject> plattformsC;
    [SerializeField] private List<GameObject> interactablesC1;
    [SerializeField] private List<GameObject> interactablesC2;
    
    [Header("Collectables")]
    [SerializeField] private List<GameObject> collect;

    [Header("Exits")]
    [SerializeField] private List<GameObject> exits;
    private IDictionary<int, List<GameObject>> interactablesF;
    private IDictionary<int, List<GameObject>> interactablesS;
    private IDictionary<int, List<GameObject>> interactablesU;
    private IDictionary<int, List<GameObject>> interactablesC;
    private IDictionary<string, List<GameObject>> background;
    private IDictionary<string, IDictionary<int, List<GameObject>>> interactables;
    private IDictionary<string, List<GameObject>> fittingExits;

    private string currentTheme;
    
    public IDictionary<string, int> stats;
    private IDictionary<string, GameObject> collectableImage;

    public static string[] themes = {"overworldNormal", "overworldForrest", "sky", "underground","castle"};
    public IDictionary<int, string[]> specificThemes;
    public static string[] collactables = {"wood", "stone", "charcoal", "whool", "iron", "coin"};

    private PlayerStats playerStats;
    private void Awake(){

    playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    
    specificThemes = new Dictionary<int, string[]>{
        {0, new string[]{themes[2]}},
        {1, new string[]{themes[0], themes[1], themes[4]}},
        {2, new string[]{themes[3]}}
    };
    
    collectableImage = new Dictionary<string, GameObject>();
    fittingExits = new Dictionary<string, List<GameObject>>{
        {"overworldNormal", new List<GameObject>{exits[0], exits[1], exits[2]}},
        {"overworldForrest", new List<GameObject>{exits[0], exits[1], exits[2]}},
        {"sky", new List<GameObject>{exits[1]}},
        {"underground", new List<GameObject>{exits[0]}},
        {"castle", new List<GameObject>{exits[0], exits[1]}}
    };
    
    background = new Dictionary<string, List<GameObject>> {
        {"overworldNormal", plattformsN},
        {"overworldForrest", plattformsF},
        {"sky", plattformsS},
        {"underground", plattformsU},
        {"castle", plattformsC}
    };

    interactablesN = new Dictionary<int, List<GameObject>>{
        {1, interactablesN1},
        {2, interactablesN2}
    };

    interactablesU = new Dictionary<int, List<GameObject>>{
        {1, interactablesU1},
        {2, interactablesU2}
    };

    interactablesF = new Dictionary<int, List<GameObject>>{
        {1, interactablesF1},
        {2, interactablesF2}
    };

    interactablesS = new Dictionary<int, List<GameObject>>{
        {1, interactablesS1},
        {2, interactablesS2}
    };

    interactablesC = new Dictionary<int, List<GameObject>>{
        {1, interactablesC1},
        {2, interactablesC2}
    };

    interactables = new Dictionary<string, IDictionary<int, List<GameObject>>> {
        {themes[0], interactablesN},
        {themes[1], interactablesF},
        {themes[2], interactablesS},
        {themes[3], interactablesU},
        {themes[4], interactablesC}
    };

    stats = new Dictionary<string, int>();
    int c = 0;
    foreach(string material in collactables){
        stats.Add(material, 0);
        collectableImage.Add(material, collect[c]);
        c++;
    }
    }

    public int getAmountOfMaterial(string material){
        return stats[material];
    }

    public IEnumerator changeAmountOfMaterial(string material, int amount){
        stats[material] += amount;
        Debug.Log(material + ": " + stats[material]);
        GameObject.Find("PlayerStats").GetComponent<PlayerStats>().changeAmountOfMaterial(material, amount);
        if(amount != 0){
        GameObject z = Instantiate(collectableImage[material], collectableImage[material].GetComponent<RectTransform>().anchoredPosition,Quaternion.identity);
        z.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").transform);
        z.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
        z.transform.localScale = new Vector3(1,1,1);
        if(amount > 0){
            z.GetComponentInChildren<TextMeshProUGUI>().text = "+ " + amount;
        }
        else{
            z.GetComponentInChildren<TextMeshProUGUI>().text = "- " + amount;
        }
        yield return new WaitForSeconds(0.5f);
        }

        
    }

    public IDictionary<int, List<GameObject>> getSpawnableInteractables(string theme){
        return interactables[theme];
    }

    public List<GameObject> getBackgroundPlattforms(string theme){
        return background[theme];
    }

    public GameObject chooseExit(string theme){
        
        return fittingExits[theme][Random.Range(0,fittingExits[theme].Count)];
    }

    public string chooseTheme(int layer){
        currentTheme = specificThemes[layer][Random.Range(0,specificThemes[layer].Length)];
        return currentTheme;
    }

    public void setCurrentTheme(string theme){
        currentTheme = theme;
    }
    public int getCurrentLayer(){
        for(int x = 0; x< 3;x++){
            int z = 0;
            for(z = 0; z < specificThemes[x].Length; z++){
                if(currentTheme.Equals(specificThemes[x][z])){
                    z = 4;
                    break;
                }
                else Debug.Log(x + ": " + z);
            }
            if(z == 4){
                Debug.Log(x);
                return x;
            }
        }
        return 1;
    }

}
