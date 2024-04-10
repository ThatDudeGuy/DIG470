using UnityEngine;

public class follow_Knight : MonoBehaviour
{
    public GameObject knight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when something is true
        transform.localPosition = new Vector3(knight.transform.localPosition.x, knight.transform.localPosition.y, -1.6f);
    }
}
