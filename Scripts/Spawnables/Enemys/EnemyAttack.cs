using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    private health playerHealth;
    private bool attacking = false;
    //////////////////////////////////////////init
    private void Awake(){
        playerHealth = GameObject.Find("Player").GetComponent<health>();
    }

    /////////////////////////////////startet Angriff Animation
    public void Attack(){
        gameObject.GetComponent<Animator>().SetTrigger("attack");
        attacking = true;    
    }

    public void EndAttack(){
        attacking = false;
    }

    ////////////////////////////////////////////Spieler nimmt nimmt Schaden
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag.Equals("Player") && attacking){
            playerHealth.TakeDamage(damage);
        }
    }
}
