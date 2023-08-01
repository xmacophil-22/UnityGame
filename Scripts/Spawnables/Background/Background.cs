using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : Spawnables
{
    private List<GameObject> plattforms;
    private int roomlength, levellength;
    private int[] plattformHight;
    private Standarts standarts;

    ///////////////////Spawns background with given area
    public void setBackground(string theme, int roomlength, int levellength, GameObject gameManager){

        standarts = gameManager.GetComponent<Standarts>();
        plattforms = standarts.getBackgroundPlattforms(theme);
        this.roomlength = roomlength;
        this.levellength = levellength;
        plattformHight = new int[roomlength*levellength];
        for(int position = 0; position < plattformHight.Length; position++){
            plattformHight[position] = Random.Range(1,3);
        }
        plattformHight[0] = 0;
        plattformHight[1] = 0;
        plattformHight[roomlength*levellength - 2] = 0;
        plattformHight[roomlength*levellength - 1] = 0;
        for(int a = 0; a < levellength; a++){
            SpawnBackground(a, theme);
        }
        //SpawnInteractabless
        
        gameObject.GetComponent<Interactables>().SpawnInteractables(plattformHight, theme);
    }

    //////////////////Spawns rooms
    private void SpawnBackground(int room, string theme){
        for(int i = 0; i < roomlength; i++){
            GameObject x = Instantiate(plattforms[0], new Vector3(((-8.5f) + 4.25f*i)+ room * 21, -3.72f, 0),Quaternion.identity);
            x.transform.parent = GameObject.Find("Terrain").transform;
            if(plattformHight[i + (room * roomlength)] == 2){
                GameObject z = Instantiate(plattforms[1], new Vector3(((-8.5f) + 4.25f*i) + room * 21, -0.86f, 0),Quaternion.identity);
                z.transform.parent = GameObject.Find("Terrain").transform;
            }
        }
        
    }
    public override int instantiateMe(){
        return 1;
    }
}
