using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private health playerHealth;
    private Rigidbody2D body;
    [SerializeField] float damage;
    [SerializeField] float dragx;
    [SerializeField] float dragy;
    /////////////////////////////////Initialisierung
    private void Awake(){
        playerHealth = GameObject.Find("Player").GetComponent<health>();
        body = playerHealth.gameObject.GetComponent<Rigidbody2D>();
    }
    ////////////////////////////////////////////verursacht Schaden bei Berührung
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.tag.Equals("Player")){
            dealDamage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.tag.Equals("Player")){
            dealDamage();
        }
    }

    private void dealDamage(){
        playerHealth.TakeDamage(damage);
        body.velocity = new Vector2(body.velocity.x + dragx, body.velocity.y + dragy);
    }
}
