using UnityEngine;
using Cinemachine;
public class KnightMovement : MonoBehaviour
{
    public Animator animator, playerAnimator;
    public Rigidbody2D playerRigidBody;
    public Collider2D[] colliders;
    private CinemachineVirtualCamera otherCamera, playerCamera;
    public float speed = 0.5f, timer = 0f, timerInterval = 2f, pushPower = 5f;
    public bool move = false, attack = false, push = false;
    Vector3 playPos;
    public AudioSource[] sounds;
    // private bool rightFace = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        otherCamera = GameObject.Find("Camera_Looks_Here").GetComponent<CinemachineVirtualCamera>();
        playerCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();

    }
    public void cameraZoomIn(){
        playerCamera.m_Lens.OrthographicSize = 2f;
        GameObject.Find("battleMusic").GetComponent<AudioSource>().Pause();
    }
    public void cameraZoomOut(){
        playerCamera.m_Lens.OrthographicSize = 5f;
    }
    public void playBattleMusic(){
        GameObject.Find("battleMusic").GetComponent<AudioSource>().Play();
    }
    public void deathGrunt(){
        sounds[2].Play();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Player")){
            return;
        }
        // else if(other.gameObject.CompareTag("Player")){
        //     animator.SetBool("Attack", true);
        // }
        // //Check for a match with the specified tag on any GameObject that collides with your GameObject
        if(animator.GetBool("Attack") == true && playerAnimator.GetBool("Dashing")){
            // if(playerAnimator.GetBool("Dashing") == true){
                playerRigidBody.velocity = Vector2.zero;
                animator.SetBool("Death", true);
                GameObject.Find("theEnd").GetComponent<Collider2D>().isTrigger = true;
            // }
            // else if(playerAnimator.GetBool("Dashing") == false){
                
            // }
        }
        else{
            // print("hello");
            playPos = playerAnimator.transform.position;
            push = true;

        }
        
    }
    public void setSpriteRenderer(){
        playerAnimator.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void setCollapse(){
        animator.SetBool("Collapse", true);
        sounds[3].Play();
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
        // if(Input.GetKeyDown(KeyCode.K)){
        //     // print("Attacking");
        //     animator.SetBool("Attack", true);
        // }
        if(playPos.x + 5f > playerAnimator.transform.position.x && push && !animator.GetBool("Death")){
            playerAnimator.transform.position += pushPower * Time.deltaTime * Vector3.right;
        }
        else{
            push = false;
        }
        if(animator.GetBool("Death")){
            foreach(Collider2D collider in colliders){
                collider.enabled = false;
            }
            // GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }
    void FixedUpdate() {
        // if(!sounds[1].isPlaying && attack) animator
        if(!animator.GetBool("Death")){
            if(timer == 0f && !sounds[1].isPlaying && attack){ 
                animator.SetBool("Attack", true);
                attack = false;
                otherCamera.Priority = -10;
                timer += Time.deltaTime;

            }
            else if(timer >= timerInterval){ 
                timer = 0f;
                animator.SetBool("Attack", false);
                attack = true;
            }
            else if(timer != 0f){
                // print("hello");
                timer += Time.deltaTime;
                
            }
        }

        if(move && !sounds[0].isPlaying){
           walkForward();
        }
    }

    public void walkForward(){
        //never use a while loop
        animator.SetBool("Moving", true);
        transform.position += speed * Time.deltaTime * Vector3.right;
        // print(transform.position.x - playerAnimator.gameObject.transform.localPosition.x);
        if(transform.position.x - playerAnimator.gameObject.transform.localPosition.x >= 0f){ 
            move = false;
            animator.SetBool("Moving", false);
            sounds[1].Play();
            attack = true;
            playerAnimator.GetComponent<PlayerMovement>().startDash = false;
            GameObject.Find("battleMusic").GetComponent<AudioSource>().PlayDelayed(3);
            // timer += Time.deltaTime;
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
