using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitCastle : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            StartCoroutine(waitasec());
        }
        
       
    }
    ///////////////////////////////////////////////beim Berühren der Tür, der Burg, wird eine Burgtheme geladen
    private IEnumerator waitasec(){
        yield return new WaitForSeconds(0.2f);
        GameObject gameManager = GameObject.Find("GameManager");
        Standarts standarts = gameManager.GetComponent<Standarts>();
        standarts.setCurrentTheme("castle");
        gameManager.GetComponent<SpawnRoom>().createRooms("castle");
    }

    
}
