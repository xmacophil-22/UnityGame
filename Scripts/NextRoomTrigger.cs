using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomTrigger : MonoBehaviour
{
    [SerializeField] private Transform preRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraMovement cam;

    //////////////////////////////////////////////////bewegt die Kamera in den nächsten Raum, wenn Trigger berührt wird
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            if(collision.transform.position.x < transform.position.x){
                cam.MoveToNextRoom(nextRoom);
            }
            else cam.MoveToNextRoom(preRoom);   
        }
    }

    public void setNextRoom(GameObject room){
        nextRoom = room.GetComponent<Transform>();
    }
    public void setCam(GameObject cam1){
        cam = cam1.GetComponent<CameraMovement>();
    }
}
