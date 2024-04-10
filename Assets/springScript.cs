using UnityEngine;

public class springScript : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.position.y > transform.position.y) {
            print("Hello");
            animator.StartPlayback();
            // animator.SetBool("Push", true);
            animator.Play("spring");
        }
    }
    // private void OnCollisionExit2D(Collision2D other) {
    //     if (other.gameObject.CompareTag("Player")) {
    //         animator.SetBool("Push", false);
    //     }
        
    // }


    public void pushUp(){
        player.GetComponent<PlayerMovement>().rb.AddForce(Vector3.up * 1f, ForceMode2D.Force);
    }
}
