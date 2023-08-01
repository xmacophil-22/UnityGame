using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitdown : Exits
{
    ////////////////////////////////////////////////////Loch als Ausgang
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            Physics2D.IgnoreLayerCollision(0,8);
        }
    }

    
}
