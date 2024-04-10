using UnityEngine;

public class Wall_Behaviour : MonoBehaviour
{
    public GameObject player, knight;
    private PlayerMovement playerStats;

    private void Start() {
        playerStats = player.GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && name == "entranceCollider_endRoom"){
            playerStats.can_I_Move = false;
            foreach(AnimatorControllerParameter parameter in player.GetComponent<Animator>().parameters){
                player.GetComponent<Animator>().SetBool(parameter.name, false);
            }
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            knight.GetComponent<KnightMovement>().move = true;
            playerStats.audioSource.Stop();
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        Vector2 collisionNormal = other.contacts[0].normal;
        if(other.gameObject.CompareTag("Player")){
            if (collisionNormal.x < 0) {
                // print("On Right");
                playerStats.rb.AddForce(Vector2.right * playerStats.speed, ForceMode2D.Force);
                playerStats.rb.gravityScale = 5;
                // Debug.Log("Player collided with the left side");
            } else if (collisionNormal.x > 0) {
                // print("On Left");
                playerStats.rb.AddForce(Vector2.left * playerStats.speed, ForceMode2D.Force);
                playerStats.rb.gravityScale = 5;
                // Debug.Log("Player collided with the right side");
            }
            // playerStats.move.x = 0;
            // print("Hi Player");
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            playerStats.rb.gravityScale = 2;
        }
    }
    // void Update()
    // {
        //write code that will push the player slightly away from the wall so that
        //the player is not stuck when moving into the wall

        //or set the players movespeed to 0 when trying to move into the wall



    // }
}
