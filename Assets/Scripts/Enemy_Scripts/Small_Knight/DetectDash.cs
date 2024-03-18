using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDash : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D shortBox;
    public Animator animator, playerAnimator;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D shortBox)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (shortBox.gameObject.name == "Girl")
        {
            print("From DetectDash");
            if(playerAnimator.GetBool("Dashing") == true){
                animator.SetBool("Damage", true);
            }
            else if(playerAnimator.GetBool("Dashing") == false){
                //playerAnimator.SetBool("Hurt", true);
                playerAnimator.SetBool("Death", true);
            }
        }
    }
}
