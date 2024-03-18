using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    Vector3 PlayerCoordinates;
    public Transform tform;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Girl");
        tform = Player.GetComponent<Transform>();
        PlayerCoordinates = tform.localPosition;
        transform.localPosition = new Vector3(PlayerCoordinates.x, PlayerCoordinates.y, -10);
        //public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = Time.deltaTime);
    }

    void FixedUpdate(){
        PlayerCoordinates = tform.localPosition;
        transform.localPosition = new Vector3(PlayerCoordinates.x, PlayerCoordinates.y, -10);//Player.localPosition
    }
}
