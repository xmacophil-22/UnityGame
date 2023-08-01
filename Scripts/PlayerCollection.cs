using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    private List<GameObject> currentCollisions = new List<GameObject>();
    public List<GameObject> currentColliders = new List<GameObject>();
    private bool isrunning = false;

    private connection connection;

    private void Awake(){
        connection = GameObject.Find("Connection").GetComponent<connection>();
    }

        //// MOVMENT interact

    public void interact(){
        if(!isrunning){
            StartCoroutine(executeAfterTime());
        }
    }

/////////////////////////////////////////////////////erstellt liste mit Kkllidierenden Objekten
    private void OnCollisionEnter2D(Collision2D collision){
        GameObject game = collision.gameObject;
        if(game.tag.Equals("collectablemov") && !currentColliders.Contains(game)){
            currentColliders.Add(game);
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        currentColliders.Remove(collision.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision){
        GameObject game = collision.gameObject;
        if(game.tag.Equals("collectable") && !currentCollisions.Contains(game)){
            currentCollisions.Add(game);
        }
        if(game.tag.Equals("bounce")){
            game.GetComponentInParent<Animator>().SetTrigger("touch");
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        currentCollisions.Remove(collision.gameObject);
    }

    /////////////////////////////////////////nach ablauf des Ladebalkens aufheben

    private IEnumerator executeAfterTime(){
        isrunning = true;
        
        List<GameObject> destroyed = new List<GameObject>();
        
        for(int y = 0; y <currentCollisions.Count; y++){
                Collactables collecta = currentCollisions[y].GetComponent<Collactables>();
                yield return collecta.StartCoroutine(collecta.collect(returnValue => {
                    if(returnValue){
                        if(currentCollisions[y] != null){
                            destroyed.Add(currentCollisions[y]);
                            currentCollisions[y].GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                }));
                
            }
            foreach(GameObject g in destroyed){
                    currentCollisions.Remove(g);
                    Destroy(g);
                   
                }
            for(int x = 0; x<currentColliders.Count; x++){
                currentColliders[x].GetComponent<CollectablesMoving>().collect();
                yield return new WaitForSeconds(0.5f);
            }
        isrunning = false;
        connection.ePressed = false;

    }
    
}
