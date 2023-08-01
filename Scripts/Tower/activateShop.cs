using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateShop : MonoBehaviour
{
    [SerializeField] private GameObject image;
    private bool collisionPlayer = false;
    private connection connection;

    /////////////////////////////////////////////////////Activiert Shop
    private void OnTriggerEnter2D(Collider2D collision){
        GameObject game = collision.gameObject;
        if(game.tag.Equals("Player")){
            collisionPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        GameObject game = collision.gameObject;
        if(game.tag.Equals("Player")){
            collisionPlayer = false;
        }
    }

    void Start(){
        connection = GameObject.Find("Connection").GetComponent<connection>();
    }

    void Update()
    {
        if(connection.ePressed && collisionPlayer){
            image.SetActive(true);
        }
    }
}
