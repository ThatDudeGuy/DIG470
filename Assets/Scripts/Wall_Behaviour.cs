using UnityEngine;
using Cinemachine;
public class Wall_Behaviour : MonoBehaviour
{
    public GameObject player;
    private GameObject knight;
    private PlayerMovement playerStats;
    public bool endGame = false;
    public AudioSource gameMusic;

    private void Start() {
        playerStats = player.GetComponent<PlayerMovement>();
        if(name == "entranceCollider_endRoom"){
            knight = GameObject.Find("KnightEnemy");
            // myCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            // otherCamera = GameObject.Find("Camera_Looks_Here").GetComponent<CinemachineVirtualCamera>();
            // myCamera.enabled = true;
            // otherCamera.enabled = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && name == "entranceCollider_endRoom" && endGame == false){
            playerStats.can_I_Move = false;
            //endGame = true;
            GameObject.Find("Camera_Looks_Here").GetComponent<follow_Knight>().setCamera_on_off();
            foreach(AnimatorControllerParameter parameter in player.GetComponent<Animator>().parameters){
                player.GetComponent<Animator>().SetBool(parameter.name, false);
            }
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // player.transform.position = transform.position;
            knight.GetComponent<KnightMovement>().move = true;
            knight.GetComponent<KnightMovement>().sounds[0].Play();
            playerStats.audioSource.Stop();
            GameObject.Find("blockDoor").GetComponent<Collider2D>().isTrigger = false;
            // myCamera.enabled = !myCamera.enabled;
            // otherCamera.enabled = !otherCamera.enabled;
            //i want to subtract the volume by time.deltaTime to get a fadeout effect
            // gameMusic.volume = 0;
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
        }
        if(other.gameObject.CompareTag("Player") && name == "floorCollider"){
            other.gameObject.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            playerStats.rb.gravityScale = 2;
        }
    }
    void Update()
    {
        if(name == "entranceCollider_endRoom" && knight.GetComponent<Animator>().GetBool("Attack")){
            endGame = true;
        }   
        else{
            return;
        }


    }
}
