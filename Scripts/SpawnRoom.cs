using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private GameObject cam;
    [SerializeField] private int levelLength;
    [SerializeField] private int roomlength;
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject exitPrefab;
    private Transform terrain;
    private Standarts standarts;
    private bool first = false;
    private PlayerStats playerStats;


    private void Start(){
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        standarts = gameObject.GetComponent<Standarts>();
        terrain = GameObject.Find("Terrain").transform;
        createRooms(standarts.chooseTheme(1));
    }

    private void Awake(){
        Player = GameObject.Find("Player").GetComponent<Transform>();
        Player.position = new Vector3(-6,0,-1);
    }

    ///////////////////////////////////////////////////////Spawnt neue Räume nach Thema und fügt Zusatzrohstoffe hinzu
    public void createRooms(string theme){
        if(first){
            playerStats.addHelperStuff();
        }
        first = true;
        int childs = terrain.childCount;
            for (int i = 0; i < childs; i++)
            {
                GameObject.Destroy(terrain.GetChild(i).gameObject);
            }
        if(Player != null){
        Player.position = new Vector3(-6,0,-1);
        }
        cam.gameObject.GetComponent<CameraMovement>().resetNextRoomPosition();
        cam.transform.position = new Vector3(0,0,-10);
        for (int i = 0; i < Standarts.themes.Length; i++)
        {
            if(theme.Equals(Standarts.themes[i])){
                cam.transform.GetChild(i).gameObject.SetActive(true);
                GameObject lantern1 = Player.GetChild(7).gameObject;
                GameObject lantern2 = Player.GetChild(8).gameObject;
                lantern1.SetActive(false);
                lantern2.SetActive(false);
                if(i > 2){
                    lantern1.SetActive(true);
                    lantern2.SetActive(true);
                }
                if(i == 1){
                    lantern2.SetActive(true);
                }
            }
            else{
                cam.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        GameObject createdRoom = null;
        NextRoomTrigger door = null;
        for(int i = 0; i < levelLength; i++){
            createdRoom = Instantiate(roomPrefab, new Vector3(i * 21, 0, 0),Quaternion.identity);
            createdRoom.transform.parent = GameObject.Find("Terrain").transform;
            if(door != null){
                door.setNextRoom(createdRoom);
                door.setCam(cam);
            }
            door = createdRoom.GetComponent<Transform>().Find("Trigger").GetComponent<NextRoomTrigger>();
            if(i == levelLength -1){
                createdRoom.GetComponent<Transform>().Find("Border").GetComponent<BoxCollider2D>().enabled = true;
                GameObject specific = standarts.chooseExit(theme);
                GameObject exit = Instantiate(specific, new Vector3(i*21 + 8f, specific.transform.position.y, specific.transform.position.z),Quaternion.identity);
                exit.transform.parent = GameObject.Find("Terrain").transform;
            }
        }

        this.gameObject.GetComponent<Background>().setBackground(theme, roomlength, levelLength, this.gameObject);
        
    }
}
