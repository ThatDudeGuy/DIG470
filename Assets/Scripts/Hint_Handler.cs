using UnityEngine;

public class Hint_Handler : MonoBehaviour
{
    public GameObject[] hints, keyImages, collisionBoxes;
    public GameObject levelMusic, battleMusic, endWall, knight;
    public bool pressA = false, pressD = false, playFinalHint = false;
    private Wall_Behaviour endCheck;
    private PlayerMovement checkPoints;
    public float timer = 0f, startVolume = 0.25f, lowVolume = 0.1f;
    void Start()
    {
        GameObject.Find("mc").GetComponent<PlayerMovement>().can_I_Move = false;
        foreach(GameObject key in keyImages){
            key.GetComponent<SpriteRenderer>().enabled = false;
        }
        endCheck = GameObject.Find("entranceCollider_endRoom").GetComponent<Wall_Behaviour>();
        checkPoints = GameObject.Find("mc").GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) hints[3].GetComponent<AudioSource>().Play();
        if(pressA && pressD){
            if(!checkPoints.atGate){
                timer += Time.deltaTime;
                if(!hints[1].GetComponent<AudioSource>().isPlaying) levelMusic.GetComponent<AudioSource>().volume = startVolume;
            } 
            if(timer >= 14){
                timer = 0;
                hints[1].GetComponent<AudioSource>().Play();
                keyImages[2].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if(hints[1].GetComponent<AudioSource>().isPlaying) levelMusic.GetComponent<AudioSource>().volume = lowVolume;
        if(checkPoints.atSpring){
            timer += Time.deltaTime;
            if(!hints[2].GetComponent<AudioSource>().isPlaying) levelMusic.GetComponent<AudioSource>().volume = startVolume;
            if(timer >= 14){
                timer = 0;
                hints[2].GetComponent<AudioSource>().Play();
                // keyImages[2].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if(hints[2].GetComponent<AudioSource>().isPlaying) levelMusic.GetComponent<AudioSource>().volume = lowVolume;
        if(playFinalHint){
            keyImages[3].GetComponent<SpriteRenderer>().enabled = true;
            hints[3].GetComponent<AudioSource>().Play();
            playFinalHint = false;
        }
        if(hints[3].GetComponent<AudioSource>().isPlaying) battleMusic.GetComponent<AudioSource>().volume = lowVolume;
        else if(!hints[3].GetComponent<AudioSource>().isPlaying && endWall.GetComponent<Wall_Behaviour>().endGame == true){
            checkPoints.can_I_Move = false;
            checkPoints.walking = true;
            battleMusic.GetComponent<AudioSource>().volume = startVolume;
        }
        if(Input.GetKeyDown(KeyCode.A) && !pressA){ 
            pressA = true;
            Destroy(keyImages[0]);
            // keyImages[0].GetComponent<SpriteRenderer>().enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.D) && !pressD){ 
            pressD = true;
            Destroy(keyImages[1]);
            // keyImages[1].GetComponent<SpriteRenderer>().enabled = false;
        }
        if(pressA && pressD && checkPoints.atBeginning){
            GameObject.Find("mc").GetComponent<PlayerMovement>().can_I_Move = true;
        }
        if(knight.GetComponent<Animator>().GetBool("Death")) Destroy(keyImages[3]);
    }
}