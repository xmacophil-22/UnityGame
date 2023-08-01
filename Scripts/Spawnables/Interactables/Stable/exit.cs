using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : Exits
{   
    private bool drive = false;
    [SerializeField] private float exitspeed;
    private void OnCollisionEnter2D(Collision2D collison){
        if(collison.gameObject.tag == "Player"){
            drive = true;
        }
        
    }
    //////////////////////////////////////////////////////Aufzugausgang fährt hoch und lädt neues Thema
    private void Update(){
        if(drive){
        transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.position.x, 5f), exitspeed * Time.deltaTime);
        if(transform.position.y > 4.9f){
            GameObject gameManager = GameObject.Find("GameManager");
            Standarts standarts = gameManager.GetComponent<Standarts>();
            gameManager.GetComponent<SpawnRoom>().createRooms(standarts.chooseTheme(standarts.getCurrentLayer() -1));
        }
        }
    }
}
