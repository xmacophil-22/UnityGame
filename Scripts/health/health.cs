using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private Image healthBar;
    

    private void Awake(){
        currentHealth = maxHealth;
    }

////////////////////////////////Spieler bekommt Schaden und Lebensanzeige wird kürzer
    public void TakeDamage(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        GameObject.Find("HealthbarCurrent").GetComponent<Image>().fillAmount = currentHealth / maxHealth;
        if(currentHealth > 0){
            //hit
        }
        else{
            SceneManager.LoadScene("Home");
            Destroy(gameObject);
        }
    }
}
