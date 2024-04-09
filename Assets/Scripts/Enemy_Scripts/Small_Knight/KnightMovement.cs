using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    public Animator animator, playerAnimator;
    public float speed;
    public int direction = 1;
    private bool rightFace = true;
    Rigidbody2D rb;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        speed = 100;
        rb = GetComponent<Rigidbody2D>();
        position = transform.localPosition;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player")){
            return;
        }
        animator.SetBool("Attack", true);
        //Check for a match with the specified tag on any GameObject that collides with your GameObject
        if(animator.GetBool("Attack") == true && playerAnimator.GetBool("Dashing")){
            if(playerAnimator.GetBool("Dashing") == true){
                animator.SetBool("Death", true);
            }
            else if(playerAnimator.GetBool("Dashing") == false){
                
            }
        }
    }
    void flip(){
        rightFace = !rightFace;
        Vector3 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    // void FixedUpdate(){
    //     if(animator.GetBool("Death") == false){
    //         // if(Input.GetKeyDown("p")){
    //         //     direction = 0;
    //         //     animator.SetBool("Moving", false);
    //         // }
    //         if(direction > 0 || direction < 0){
    //             animator.SetBool("Moving", true);
    //             //print("Moving");
    //         }
    //         else{
    //             animator.SetBool("Moving", false);
    //         }

    //         if(rightFace == false && direction > 0){
    //             flip();
    //         }
    //         else if(rightFace == true && direction < 0){
    //             flip();
    //         }

    //         if(animator.GetBool("Attack") == false){
    //             //animator.SetBool("Moving", false);
    //             rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
    //         }
    //         else{
    //             direction = 0;
    //         }

    //         // if(animator.GetBool("Death") == true){
                
    //         // }
            
    //         //print(transform.localPosition);
    //         position = transform.localPosition;
    //         if(position.x >= 2.5 && rightFace == true){
    //             direction *= -1;
    //         }
    //         else if(position.x <= -2.5 && rightFace == false){
    //             direction *= -1;
    //         }
            
    //     }
    // }
}
