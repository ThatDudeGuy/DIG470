using UnityEngine;
using Cinemachine;
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
    public bool rightFace = true, isGrounded, isJumping, startDash, dashAgain = true, can_I_Move = true, walking = false; //startSlide;
    public bool playOnce = true, atBeginning = false, atGate = false, atSpring = false;
    public Vector2 move;
    public Rigidbody2D rb;
    public GameObject mainMusic, end_wall_Collider, knight;
    public Hint_Handler hints;
    void Start()
    {
        playerHealth = 3;
        Application.targetFrameRate = 60;
        jumpTime = 0.25f;
        speed = 250;
        jumpForce = 200;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // print(other.gameObject.name);
        if(other.gameObject.name == hints.collisionBoxes[0].name & !hints.pressA){
            hints.keyImages[0].GetComponent<SpriteRenderer>().enabled = true;
            hints.keyImages[1].GetComponent<SpriteRenderer>().enabled = true;
            hints.keyImages[0].GetComponent<Animator>().SetBool("A", true);
            hints.keyImages[1].GetComponent<Animator>().SetBool("D", true);
            hints.hints[0].GetComponent<AudioSource>().Play();
            mainMusic.GetComponent<AudioSource>().volume /= 4;
            atBeginning = true;
        }
        else if(other.gameObject.name == hints.collisionBoxes[1].name){
            atGate = true;
            Destroy(hints.keyImages[2]);
        }
        else if(other.gameObject.name == hints.collisionBoxes[2].name){
            atSpring = true;
        }
        else if(other.gameObject.name == hints.collisionBoxes[3].name){
            atSpring = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name == hints.collisionBoxes[0].name & hints.pressA){
            mainMusic.GetComponent<AudioSource>().volume *= 4;
            atBeginning = false;
        }

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
        if (collision.gameObject.name == "KnightEnemy"){
            // playPos = playerAnimator.transform.position;
            rb.velocity = Vector2.zero;
            GameObject.Find("KnightEnemy").GetComponent<KnightMovement>().push = true;
            GameObject.Find("KnightEnemy").GetComponent<KnightMovement>().num_of_Attempts += 1;
        }
        // if(collision.gameObject.name == "theEnd") go to next screen
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
        if(atBeginning && Input.GetKeyDown(KeyCode.P)){
            hints.hints[0].GetComponent<AudioSource>().Stop();
            hints.hints[0].GetComponent<AudioSource>().Play();
        }
        if(can_I_Move){
            // if(mainMusic.GetComponent<AudioSource>().volume < 0.2f)
            //     mainMusic.GetComponent<AudioSource>().volume += Time.deltaTime;
            if(animator.GetBool("Death") == false){
                // if(Input.GetKeyDown("m")){
                //     animator.SetBool("Death", true);
                // }
                move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                
                if(move.x > 0 || move.x < 0){
                    animator.SetBool("Moving", true);
                    walking = true;
                    if(isGrounded && playOnce){
                        audioSource.Play();
                        playOnce = false;
                    }
                }
                else{
                    // move = Vector2.zero;
                    walking = false;
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
                if(Input.GetKeyDown("space") && isGrounded == true){
                    isJumping = true;
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
                        jumpTimeCounter -= Time.deltaTime;
                        animator.SetBool("Falling", true);
                    }
                    else{
                        isJumping = false;
                    }
                }

                //The math here is defined as move.x = (-1 to 1) * speed * 

                
                // jump(jumpForce);

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

                // dash(startDash, currentY.y);
                //backDash(start_backDash, currentY.y);

                //for the dash
                if(move.x < 0 && increment > 0){
                    increment = increment * -1;
                }
                else if(move.x > 0 && increment < 0){
                    increment = increment * -1;
                }
            }
        }
        // else{
        //     // if(mainMusic.GetComponent<AudioSource>().volume > 0)
        //     //     mainMusic.GetComponent<AudioSource>().volume -= Time.deltaTime;
        //     mainMusic.GetComponent<AudioSource>().Stop();
        // }

        //dumb solution
        if(end_wall_Collider.GetComponent<Wall_Behaviour>().endGame == true && !knight.GetComponent<KnightMovement>().push){
            // move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
            // rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
            if(Input.GetKeyDown("e") && dashAgain){
                    startDash = true;
                    currentY = transform.localScale;
                    timer += Time.deltaTime;
            }
            if(timer == 0f && !animator.GetBool("Jumping")) dashAgain = true;
            else if(timer >= timerInterval) timer = 0f;
            else if(timer != 0f){
                timer += Time.deltaTime;
                dashAgain = false;
            }
            //for the dash
            if(move.x < 0 && increment > 0){
                increment = increment * -1;
            }
            else if(move.x > 0 && increment < 0){
                increment = increment * -1;
            }
            // dash(startDash, currentY.y);
        }
    }
    void FixedUpdate() {
        if(end_wall_Collider.GetComponent<Wall_Behaviour>().endGame == true && !knight.GetComponent<KnightMovement>().push && GetComponent<SpriteRenderer>().enabled && !can_I_Move){
            if(walking) move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y); 
            if(startDash) dash(startDash, currentY.y);  
        }
        // else{
            if(can_I_Move && !end_wall_Collider.GetComponent<Wall_Behaviour>().endGame){
                if(walking) rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y); 
                else rb.velocity = new Vector2(0f, rb.velocity.y);
                if(isJumping) jump(jumpForce);
                if(startDash) dash(startDash, currentY.y);  
            }  
        // } 
         
        
    }

    void flip(){
        rightFace = !rightFace;
        Vector3 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    void jump(float force){
        if(isGrounded == true){
            // isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, force * Time.deltaTime);
            // jumpTimeCounter = jumpTime;
        }
        // if(Input.GetKeyUp("space") && isGrounded == false){
        //     audioSource.Stop();
        //     playOnce = true;
        //     animator.SetBool("Falling", true);
        //     isJumping = false;
        // }
        if(isGrounded == false && isJumping == true){
            if(jumpTimeCounter > 0){
            //     audioSource.Stop();
            //     playOnce = true;
                rb.velocity = new Vector2(rb.velocity.x, force * Time.deltaTime);
                // jumpTimeCounter -= Time.deltaTime;
                // animator.SetBool("Falling", true);
            }
            else{
                isJumping = false;
            }
        }
    }
    void dash(bool begin_dash, float yValue){
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