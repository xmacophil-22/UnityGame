using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject towerPart;
    [SerializeField] private GameObject towerTop;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject scoreboard;
    private PlayerStats playerStats;
    private bool collisionWithPlayer;
    private bool move;
    private Vector3 velocity = Vector3.zero;
    private Vector3 endPos;
    private Vector3 startPos;
    private connection connection;
    private PlayerMovement player;
    private bool open = false;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        connection = GameObject.Find("Connection").GetComponent<connection>();
        startPos = cam.position;
        move = false;
        collisionWithPlayer = false;
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        reload();
    }
    //////////////////////////////////////////lässt Turm je nach Score wachsen
    public void reload(){
        long highScore = playerStats.getHighScore();
        for (int i = 0; i < highScore; i++)
        {
            GameObject currentTower = Instantiate(towerPart, new Vector3(towerPart.transform.position.x, towerPart.transform.position.y+i, towerPart.transform.position.z),Quaternion.identity);
            currentTower.transform.parent = GameObject.Find("Tower").transform;
        }
        towerTop.transform.position = new Vector3(towerTop.transform.position.x, 3.14f + highScore, towerTop.transform.position.z);
        towerTop.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Score: " + highScore;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        GameObject game = collision.gameObject;
        if(game.tag.Equals("Player")){
            collisionWithPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        GameObject game = collision.gameObject;
        if(game.tag.Equals("Player")){
            collisionWithPlayer = false;
        }
    }

    ///////////////////////////////////////////////////////bei Barührung mit Spieler und E, Camera verfolgt Turm und zeigt Scores
    private void Update(){
        if(collisionWithPlayer && connection.ePressed){
            open = true;
            scoreboard.SetActive(true);
            player.enabled = false;
            GameObject.Find("RequestHandler").GetComponent<RequestHandler>().getScores();
            if(!move){
                move = true;
                endPos = new Vector3(cam.position.x, cam.position.y + playerStats.getHighScore(), cam.position.z);
            }
        }
        if(move){
            showTower();
        }
        if(connection.close && open){
            connection.close = false;
            player.enabled = true;
            close();
        }
    }
    private void close(){
        scoreboard.SetActive(false);
        cam.position = startPos;
        move = false;
        open = false;
    }

    private void showTower(){
        cam.position = Vector3.SmoothDamp(cam.position, endPos, ref velocity, 0.5f);
        connection.ePressed = false;
        if(cam.position.y > endPos.y){
            move = false;
        }
    }

}
