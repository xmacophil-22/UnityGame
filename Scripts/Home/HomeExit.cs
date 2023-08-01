using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeExit : MonoBehaviour
{
    [SerializeField] private string stoLoad;

    /////////////////////////////////////////lade unterschiedliche Szenen wenn an linke oder rechte Grenze gestoßen wird
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.tag.Equals("Player")){
            SceneManager.LoadScene(stoLoad);
        }
    }
}
