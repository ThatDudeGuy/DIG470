using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackMinatour : MonoBehaviour
{
    public Animator animator, playerAnimator;
    public Collider2D circle, box;
    public GameObject Player;
    private PlayerMovement playerScript;
    void Start()
    {
        Player = GameObject.Find("Girl");
        playerScript = Player.GetComponent<PlayerMovement>();
        circle = GetComponent<BoxCollider2D>();
        Debug.Log(playerScript.playerHealth);
        animator.SetBool("Attack", true);
        //tform = Player.GetComponent<Transform>();
    }
    void OnTriggerEnter2D(Collider2D circle)
    {
        if(!circle.CompareTag("Player")){
            return;
        }
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if(animator.GetBool("Attack") == true){
            if(playerAnimator.GetBool("Jumping") == true){
                animator.SetBool("Death", true);
            }
            // else if(playerAnimator.GetBool("Jumping") == false){
            //     playerAnimator.SetBool("Hurt", true);
            //     playerScript.playerHealth -= 1;
            //     if(playerScript.playerHealth <= 0){
            //         playerAnimator.SetBool("Death", true);
            //     }
            // }
        }
        if (circle.gameObject.name == "Girl")
        {
            animator.SetBool("Attack", true);
        }
        if(animator.GetBool("Death") == true){
            animator.SetBool("Attack", false);
        }
        
    }
    void OnTriggerExit2D(Collider2D circle){
        playerAnimator.SetBool("Hurt", false);
    }
}
