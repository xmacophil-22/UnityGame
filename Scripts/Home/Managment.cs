using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managment : MonoBehaviour
{
    private GameObject player;
    private Transform playerTrans;

    ////////////////////////////////////reset Playerposition
    void Start()
    {
        player = GameObject.Find("Player");
        playerTrans = player.GetComponent<Transform>();
        playerTrans.position = new Vector3(-4.84f,-1.17f,-1);
    }
}
