using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private Transform healthbar;
    private Standarts standarts;
    private PlayerStats playerStats;

    /////////////////////////////////////////Gegner Initialisierung
    private void Awake(){
        currentHealth = maxHealth;
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        standarts = GameObject.Find("GameManager").GetComponent<Standarts>();
    }
    /////////////////////////////////////Gegner nimmt schaden
    public void Hit(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if(currentHealth > 0){
            float relation = currentHealth/maxHealth;
            healthbar.localScale = new Vector3(relation, healthbar.localScale.y, healthbar.localScale.z);
            healthbar.localPosition = new Vector3((healthbar.localPosition.x - (0.75f*damage)), 0,0);
        }
        else{
            healthbar.localScale = new Vector3(0, healthbar.localScale.y, healthbar.localScale.z);
            StartCoroutine(standarts.changeAmountOfMaterial("coin", 10 * (playerStats.getLevelOfMagic(0) +1)));
            Destroy(gameObject);
        }
    }

}
