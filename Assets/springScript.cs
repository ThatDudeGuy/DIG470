using UnityEngine;

public class springScript : MonoBehaviour
{
    public GameObject player;
    public AudioSource audio;
    public float pushPower = 12.5f;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        pushPower = 12.5f;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.position.y > transform.position.y) {
            // print("Hello");
            // animator.StartPlayback();
            animator.SetBool("extend", true);
            // animator.Play("spring");
        }
    }
    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.P)){
    //         print("Go UP");
    //         // animator.Play("spring");
    //         player.GetComponent<PlayerMovement>().rb.AddForce(Vector3.up * 10f, ForceMode2D.Impulse);
    //     }
    // }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            animator.SetBool("extend", false);
        }
        
    }


    public void pushUp(){
        if(animator.GetBool("extend")) player.GetComponent<PlayerMovement>().rb.AddForce(Vector3.up * pushPower, ForceMode2D.Impulse);
        audio.Play();
    }
}
