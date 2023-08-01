using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform position;
    private bool currentlyTurning = false;

    [SerializeField] private float speed;
    private float direction;
    private float currentspeed;

    ///////////////////////////////////////Bewegung der Schafe und Enemys in einem bestimmten Bereich

    private void OnCollisionEnter2D(Collision2D collison){
        if(collison.gameObject.tag == "boundaries" && !currentlyTurning){
            StartCoroutine(turnAround());
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collison){
        if(collison.gameObject.tag == "boundaries"&& !currentlyTurning){
            StartCoroutine(turnAround());
        }
    }

    private IEnumerator turnAround(){
        currentlyTurning = true;
        currentspeed = 0;
        yield return new WaitForSeconds(0.5f);
        direction = (0-direction);
        position.localScale = new Vector3(position.localScale.x * -1, position.localScale.y, position.localScale.z);
        yield return new WaitForSeconds(0.5f);
        currentspeed = direction * speed * Random.Range(0.5f, 1.0f);
        yield return new WaitForSeconds(1);
        currentlyTurning = false;
        
    }


    

    
    private void Awake(){
        body = gameObject.GetComponent<Rigidbody2D>();
        position = gameObject.GetComponent<Transform>();
        direction = -1;
        currentspeed = direction * speed;


    }

    private void Update(){
        body.velocity = new Vector2(currentspeed, body.velocity.y);
    }

}
