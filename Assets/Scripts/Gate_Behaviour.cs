using UnityEngine;

public class Gate_Behaviour : MonoBehaviour
{
    public GameObject pressurePlate, floor;
    public AudioSource[] audio;

    public const float Y_LOCATION_BOTTOM = -15.097f, Y_LOCATION_TOP = -10.7f;

    private void Update() {
        if (pressurePlate.GetComponent<Plate_Behaviour>().gateOpen) {
            if (transform.localPosition.y >= Y_LOCATION_TOP) {
                pressurePlate.GetComponent<Plate_Behaviour>().stop = true;
                audio[0].Stop();
            } else if (transform.localPosition.y > Y_LOCATION_BOTTOM) {
                pressurePlate.GetComponent<Plate_Behaviour>().stop = false;
            }
        } 
        else {
            if (transform.localPosition.y < Y_LOCATION_BOTTOM + 0.1f) {
                pressurePlate.GetComponent<Plate_Behaviour>().stop = true;
                audio[1].Stop();
            } else if (transform.localPosition.y <= Y_LOCATION_TOP) {
                pressurePlate.GetComponent<Plate_Behaviour>().stop = false;
            }
        }
        // if(pressurePlate.GetComponent<Plate_Behaviour>().gateOpen == true && transform.localPosition.y >= Y_LOCATION_TOP){
        //     pressurePlate.GetComponent<Plate_Behaviour>().stop = true;
        // }
        // else if(pressurePlate.GetComponent<Plate_Behaviour>().gateOpen == true && transform.localPosition.y > Y_LOCATION_BOTTOM && transform.localPosition.y >= Y_LOCATION_TOP){
        //     pressurePlate.GetComponent<Plate_Behaviour>().stop = false;
        // }
        // else if(pressurePlate.GetComponent<Plate_Behaviour>().gateOpen == false && transform.localPosition.y > Y_LOCATION_BOTTOM && transform.localPosition.y < Y_LOCATION_TOP){
        //     pressurePlate.GetComponent<Plate_Behaviour>().stop = false;
        // }
        // else if(pressurePlate.GetComponent<Plate_Behaviour>().gateOpen == false && transform.localPosition.y <= Y_LOCATION_BOTTOM ){
        //     pressurePlate.GetComponent<Plate_Behaviour>().stop = true;
        // }
    }
}
