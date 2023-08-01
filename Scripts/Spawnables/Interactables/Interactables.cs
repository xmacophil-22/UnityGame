using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : Spawnables
{
    private Standarts standarts;
    private IDictionary<int, List<GameObject>> spawnables;
    private int[] spawnpositions;
    //////////////////////////////////////Spawn Detailles auf der Map
    public void SpawnInteractables(int[] plattformHights, string theme){
        
        
        standarts = gameObject.GetComponent<Standarts>();
        spawnables = standarts.getSpawnableInteractables(theme);
        int[] plattformWidths = new int[plattformHights.Length];
        for(int a = 0; a < plattformHights.Length; a++){
            switch(plattformHights[a]){
                case 1: 
                    plattformWidths[a] = 0;
                    break;
                case 2:
                    if(plattformHights[a + 1] == 2){
                        plattformWidths[a] = 2;
                    }
                    else plattformWidths[a] = 1;
                    break;
                default:
                    plattformWidths[a] = 0;
                    break;
            }
        }
        
        for(int x = 2; x < plattformHights.Length -2; x++){
            int size = Random.Range(1,3);
            List<GameObject> list = spawnables[size];
            
            GameObject prefab = list[Random.Range(0,list.Count)];
            float offset = Random.Range(0.0f, 3.0f);
            GameObject z = Instantiate(prefab, new Vector3(-6.5f + 4.25f*x + offset, -1f+prefab.transform.position.y, 1f),Quaternion.identity);
            z.transform.parent = GameObject.Find("Terrain").transform;
            x = x + (size -1);
        }

        for(int x = 2; x < plattformWidths.Length -2; x++){
            if(plattformWidths[x] != 0){
            int size = Random.Range(1,plattformWidths[x]+1);
            List<GameObject> list = spawnables[size];
            GameObject prefab = list[Random.Range(0,list.Count)];
            float offset = Random.Range(0.0f, 0.5f);
            GameObject z = Instantiate(prefab, new Vector3(-8.5f + 4.25f*x + offset, 1.85f + prefab.transform.position.y, 1f),Quaternion.identity);
            z.transform.parent = GameObject.Find("Terrain").transform;
            x = x + (size -1);
            }
        }


        


    }
    public override int instantiateMe(){
        return 1;
    }
    
}
