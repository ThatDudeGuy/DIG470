using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class confirmButton : MonoBehaviour
{
    private AudioSource click;
    private bool confirmed = false;
    private void Start() {
        click = GetComponent<AudioSource>();
        if(name == "play") GetComponent<Button>().interactable = false;
    }
    public void startGame(){
        if(confirmed){
            //click.Play();
            SceneManager.UnloadSceneAsync("startScreen");
            SceneManager.LoadScene("Game");
        }      
    }
    public void confirm(){
        click.Play();
        confirmed = true;
        GameObject.Find("play").GetComponent<Button>().interactable = true;
        GameObject.Find("acknowledge").GetComponent<Button>().interactable = false;
    }
}

