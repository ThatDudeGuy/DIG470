using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public int playerHealth = 3;
    public AudioSource audioSource;
    public float speed;
    public float jumpForce, jumpTime;
    private float jumpTimeCounter, timer = 0f, timerInterval = 0.5f;
    private Vector3 currentY;
    public int dashing = 0, increment = 1, slideIncrement = 2, sliding = 0;
    public bool rightFace = true, isGrounded, isJumping, startDash, dashAgain = true, can_I_Move = true; //startSlide;
    public bool playOnce = true;
    public Vector2 move;
    public Rigidbody2D rb;
    void Start()
    {
        playerHealth = 3;
        Application.targetFrameRate = 60;
        jumpTime = 0.25f;
        speed = 250;
        jumpForce = 200;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            //animator.SetBool("Landing", true);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
            animator.SetBool("Jumping", true);
            //animator.SetBool("Landing", false);
        }
    }

    void Update() {
        if(can_I_Move){
            if(animator.GetBool("Death") == false){
                // if(Input.GetKeyDown("m")){
                //     animator.SetBool("Death", true);
                // }
                move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                if(move.x > 0 || move.x < 0){
                    animator.SetBool("Moving", true);
                    if(isGrounded && playOnce){
                        audioSource.Play();
                        playOnce = false;
                    }
                }
                else{
                    audioSource.Stop();
                    animator.SetBool("Moving", false);
                    playOnce = true;
                }

                if(rightFace == false && move.x > 0){
                    flip();
                }
                else if(rightFace == true && move.x < 0){
                    flip();
                }
                //rb.AddForce(move * speed * Time.deltaTime);
                rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);

                //The math here is defined as move.x = (-1 to 1) * speed * 
                
                jump(jumpForce);

                // if(isJumping && startDash) dashAgain = false;
                // else if(!isJumping && !startDash) dashAgain = true;

                if(Input.GetKeyDown("e") && dashAgain){
                    startDash = true;
                    currentY = transform.localScale;
                    timer += Time.deltaTime;
                }
                // if(Input.GetKeyDown("q")){
                //     start_backDash = true;
                //     currentY = transform.localScale;
                // }
                if(timer == 0f && !animator.GetBool("Jumping")) dashAgain = true;
                else if(timer >= timerInterval) timer = 0f;
                else if(timer != 0f){
                    timer += Time.deltaTime;
                    dashAgain = false;
                }

                dash(startDash, currentY.y);
                //backDash(start_backDash, currentY.y);
            }
        }
    }

    void flip(){
        rightFace = !rightFace;
        Vector3 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    void jump(float force){
        if(Input.GetKeyDown("space") && isGrounded == true){
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, force * Time.deltaTime);
            jumpTimeCounter = jumpTime;
        }
        if(Input.GetKeyUp("space") && isGrounded == false){
            audioSource.Stop();
            playOnce = true;
            animator.SetBool("Falling", true);
            isJumping = false;
        }
        if(Input.GetKey("space") && isGrounded == false && isJumping == true){
            if(jumpTimeCounter > 0){
                audioSource.Stop();
                playOnce = true;
                rb.velocity = new Vector2(rb.velocity.x, force * Time.deltaTime);
                jumpTimeCounter -= Time.deltaTime;
                animator.SetBool("Falling", true);
            }
            else{
                isJumping = false;
            }
        }
    }
    void dash(bool begin_dash, float yValue){
        if(move.x < 0 && increment > 0){
            increment = increment * -1;
        }
        else if(move.x > 0 && increment < 0){
            increment = increment * -1;
        }
        if(begin_dash == true){
            //print(dashing);
            animator.SetBool("Dashing", true);
            dashing += increment;
            rb.velocity = new Vector2(move.x + dashing * speed * Time.deltaTime, yValue);
            if(dashing >= 8 || dashing <= -8){
                startDash = false;
                dashing = 0;
                animator.SetBool("Dashing", false);
            }
        }
    }

    public void setMove(){
        can_I_Move = !can_I_Move;
    }

    // void backDash(bool begin_backDash){
    //  
    // }



    // void slide(bool begin_slide){
    //     if(move.x < 0 && slideIncrement > 0){
    //         slideIncrement = slideIncrement * -1;
    //     }
    //     else if(move.x > 0 && slideIncrement < 0){
    //         slideIncrement = slideIncrement * -1;
    //     }
    //     if(begin_slide == true){
    //         print(sliding);
    //         sliding += slideIncrement;
    //         rb.velocity = new Vector2(move.x + sliding * speed * Time.deltaTime, rb.velocity.y);
    //         if(sliding >= 8 || sliding <= -8){
    //            startSlide = false;
    //            sliding = 0;
    //         }
    //     }
    // }

     // void FixedUpdate(){
    //     if(animator.GetBool("Death") == false){
    //         if(Input.GetKeyDown("m")){
    //             animator.SetBool("Death", true);
    //         }
    //         move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //         if(move.x > 0 || move.x < 0){
    //             animator.SetBool("Moving", true);
    //         }
    //         else{
    //             animator.SetBool("Moving", false);
    //         }

    //         if(rightFace == false && move.x > 0){
    //             flip();
    //         }
    //         else if(rightFace == true && move.x < 0){
    //             flip();
    //         }
    //         //rb.AddForce(move * speed * Time.deltaTime);
    //         rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);

    //         //The math here is defined as move.x = (-1 to 1) * speed * 
            
    //         jump(jumpForce);

    //         if(Input.GetKeyDown("e")){
    //             startDash = true;
    //             currentY = transform.localScale;
    //         }
    //         if(Input.GetKeyDown("c")){
    //             startSlide = true;
    //         }
    //         dash(startDash, currentY.y);
    //         slide(startSlide);
    //         //OnCollisionEnter(ninjaCollider.OnCollisionEnter);
    //     }
    // }
}