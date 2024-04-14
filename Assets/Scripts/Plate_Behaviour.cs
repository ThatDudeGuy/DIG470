using UnityEngine;

public class Plate_Behaviour : MonoBehaviour
{
    public GameObject player, gate;
    Animator animator;
    public bool gateOpen = false, stop;
    public float openStrength = 1f, closeStrength = 4.5f;
    private void Start() {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        stop = true;
        openStrength = 1f;
        closeStrength = 4f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            animator.enabled = true;
            animator.SetBool("Activate", true);
        }
        else{
            return;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            animator.SetBool("Activate", false);
        }
        else{
            return;
        }
    }

    private void Update() {
        if(stop == false){
            //print("MOVING");
            if(gateOpen) gate.transform.position += openStrength * Time.deltaTime * Vector3.up;
            else gate.transform.position += closeStrength * Time.deltaTime * Vector3.down;
        }
    }
    public void liftGate(){
        gateOpen = true;
        gate.transform.position += openStrength * 5 * Time.deltaTime * Vector3.up;
        gate.GetComponent<Gate_Behaviour>().audio[1].Stop();
        gate.GetComponent<Gate_Behaviour>().audio[0].Play();
    }
    public void closeGate(){
        gateOpen = false;
        gate.GetComponent<Gate_Behaviour>().audio[0].Stop();
        gate.GetComponent<Gate_Behaviour>().audio[1].Play();
        if(gate.transform.position.y > Gate_Behaviour.Y_LOCATION_BOTTOM) gate.transform.position += openStrength * 2 * Time.deltaTime * Vector3.down;
    }
}
