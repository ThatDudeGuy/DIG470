using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //public GameObject Player;
   // Vector3 PlayerCoordinates;
    //public Transform tform;
    public Animator animator, playerAnimator;
    public Collider2D longBox;
    public GameObject Player;
    private PlayerMovement playerScript;
    void Start()
    {
        Player = GameObject.Find("mc");
        playerScript = Player.GetComponent<PlayerMovement>();
        longBox = GetComponent<BoxCollider2D>();
        Debug.Log(playerScript.playerHealth);
        //tform = Player.GetComponent<Transform>();
    }
    void OnTriggerEnter2D(Collider2D longBox)
    {
        if(!longBox.CompareTag("Player")){
            return;
        }
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if(animator.GetBool("Attack") == true){
            if(playerAnimator.GetBool("Dashing") == true){
                animator.SetBool("Death", true);
            }
            else if(playerAnimator.GetBool("Dashing") == false){
                playerAnimator.SetBool("Hurt", true);
                playerScript.playerHealth -= 1;
                if(playerScript.playerHealth <= 0){
                    playerAnimator.SetBool("Death", true);
                }
            }
        }
        if (longBox.gameObject.name == "mc")
        {
            animator.SetBool("Attack", true);
        }
        if(animator.GetBool("Death") == true){
            animator.SetBool("Attack", false);
        }
        
    }
    void OnTriggerExit2D(Collider2D longBox){
        playerAnimator.SetBool("Hurt", false);
    }
    // void FixedUpdate(){
    //     PlayerCoordinates = tform.localPosition;
        
    // }
}
