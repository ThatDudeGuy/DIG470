using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    public Animator animator, playerAnimator;
    public float speed = 0.5f;
    public bool move = false;
    // private bool rightFace = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        // if(!other.gameObject.CompareTag("Player")){
        //     return;
        // }
        // else if(other.gameObject.CompareTag("Player")){
        //     animator.SetBool("Attack", true);
        // }
        // //Check for a match with the specified tag on any GameObject that collides with your GameObject
        if(animator.GetBool("Attack") == true && playerAnimator.GetBool("Dashing")){
            // if(playerAnimator.GetBool("Dashing") == true){
                animator.SetBool("Death", true);
            // }
            // else if(playerAnimator.GetBool("Dashing") == false){
                
            // }
        }
        
    }
    public void setSpriteRenderer(){
        playerAnimator.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void setCollapse(){
        animator.SetBool("Collapse", true);
        playerAnimator.GetComponent<SpriteRenderer>().enabled = true;
    }
    // void flip(){
    //     rightFace = !rightFace;
    //     Vector3 Scaler = transform.localScale;
    //     Scaler.x = Scaler.x * -1;
    //     transform.localScale = Scaler;
    // }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            print("Attacking");
            animator.SetBool("Attack", true);
        }
        if(animator.GetBool("Death")){
            GetComponent<Collider2D>().enabled = false;
        }
        
    }
    void FixedUpdate() {
        if(move){
           walkForward();
        }
    }

    public void walkForward(){
        //never use a while loop
        animator.SetBool("Moving", true);
        transform.position += speed * Time.deltaTime * Vector3.right;
        print(transform.position.x - playerAnimator.gameObject.transform.localPosition.x);
        if(transform.position.x - playerAnimator.gameObject.transform.localPosition.x >= 0f){ 
            move = false;
            animator.SetBool("Moving", false);
            GetComponent<AudioSource>().Play();
        }
    }
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
