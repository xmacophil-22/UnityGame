using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private PlayerHitRange playerHitRange;
    [SerializeField] public float damage;
    private PlayerStats playerStats;

    ///////////////////////////////////////////////verbindet Schwert mit PlayerStats um Zugriff zu haben

    private void Start(){
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    //////////////////////////////////////////////verursacht Schaden, je nach Level des Schwerts
    public void makeDamage(){
        int len = playerHitRange.enemysInRange.Count;
        for (int i = 0; i < len; i++)
        {
            playerHitRange.enemysInRange[i].GetComponent<EnemyHealth>().Hit(damage * (playerStats.getWeaponUpgrade(0) + 1) * 0.2f);
        }
    }
}
