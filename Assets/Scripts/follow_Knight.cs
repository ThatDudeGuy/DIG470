using UnityEngine;
using Cinemachine;
public class follow_Knight : MonoBehaviour
{
    public GameObject knight;

    // Update is called once per frame
    private void Start() {
        GetComponent<CinemachineVirtualCamera>().enabled = false;
    }
    void Update()
    {
        //when something is true
        if(GetComponent<CinemachineVirtualCamera>().enabled)
            transform.localPosition = new Vector3(knight.transform.localPosition.x - 0.25f, knight.transform.localPosition.y, -1.6f);
    }

    public void setCamera_on_off(){
        GetComponent<CinemachineVirtualCamera>().enabled = !GetComponent<CinemachineVirtualCamera>().enabled;
        GetComponent<CinemachineVirtualCamera>().Priority *= -1;
    }
}
