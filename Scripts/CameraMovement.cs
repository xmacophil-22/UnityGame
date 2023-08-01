using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float nextRoomPosition;
    private Vector3 velocity = Vector3.zero;

    ////////////////////////////////////////bewegt die Kamera
    private void Update(){
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(nextRoomPosition, transform.position.y, transform.position.z), ref velocity, speed);
        }
    
    public void MoveToNextRoom(Transform nextRoom){
        nextRoomPosition = nextRoom.position.x;
    }

    public void resetNextRoomPosition(){
        nextRoomPosition = 0;
    }
}
