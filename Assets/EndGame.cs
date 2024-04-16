using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            SceneManager.UnloadSceneAsync("Game");
            SceneManager.LoadScene("endScreen");
        }
   }
   
}
