using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitRange : MonoBehaviour
{
    public List<GameObject> enemysInRange;
    private void Start(){
        enemysInRange = new List<GameObject>();
    }

    ////////////////////////////////////////////Sammelt Gegner in Reichweite
    private void OnTriggerEnter2D(Collider2D collider){
        GameObject t = collider.gameObject;
        if(t.tag.Equals("enemy")){
            enemysInRange.Add(t);
            Debug.Log(enemysInRange.Count);
        }
    }
    private void OnTriggerExit2D(Collider2D collider){
        GameObject t = collider.gameObject;
        if(t.tag.Equals("enemy")){
            enemysInRange.Remove(t);
            Debug.Log(enemysInRange.Count);
        }
    }
}
