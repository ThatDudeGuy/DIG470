using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Knight_Movement : MonoBehaviour
{
    public float speed = 100;
    public Rigidbody2D rb;
    public Vector2 move;
    Vector3 position;
    bool rightFace = true, keepGoing = true;
    int counter = 0;

    void Flip(){
        rightFace = !rightFace;
        Vector3 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    private void Start() {
        move.x = 1;
    }

    private void FixedUpdate() {
        if(keepGoing){
            rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
            position = transform.localPosition;
        }
        
        if(position.x >= 2.5 && rightFace == true){
            move.x *= -1;
            Flip();
            counter+=1;
        }
        else if(position.x <= -2.5 && rightFace == false){
            move.x *= -1;
            Flip();
        }
        if(counter >= 2){
            move.x = 0;
            keepGoing = false;
        }
    }
}