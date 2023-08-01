using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private EnemyAttack enemyAttack;

    //////////////////////////////////////////sagt aus, ob Spieler in Reichweite des Gegners ist
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.tag.Equals("Player")){
            enemyAttack.Attack();
        }
    }
}
