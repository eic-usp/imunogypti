using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
	public GameObject menu;
    Animator anim;
    bool paused=false;
 
    public void Pause(){
        paused=true;
    	menu.SetActive(true);
    }

    public void GoToMenu(){
    	Time.timeScale = 1f;
        paused = false;
        FindObjectOfType<AudioManager>().Pause("Stage1");
    	SceneManager.LoadScene("MainMenu");
    }

    public void Restart(){
    	Time.timeScale = 1f;
        paused = false;
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Play(){
    	menu.SetActive(false);
        paused = false;
    	Time.timeScale = 1f;
    }

    void Update(){
        if(menu==null)
            return;

        anim = menu.transform.GetChild(0).GetComponent<Animator>();
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Pause") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && paused ==true){
            Time.timeScale = 0f;
        }
    }

}
