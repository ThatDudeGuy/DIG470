using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartsScript : MonoBehaviour
{
    private GameObject Player;
    Vector3 PlayerCoordinates;
    private Transform tform;
    private PlayerMovement playerScript;
    void Start()
    {
        Player = GameObject.Find("Girl");
        tform = Player.GetComponent<Transform>();
    }

    void FixedUpdate(){
        PlayerCoordinates = tform.localPosition;
        transform.localPosition = new Vector3(PlayerCoordinates.x, PlayerCoordinates.y + 1.75f, 0);//Player.localPosition
    }
}