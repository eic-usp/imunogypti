using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
	public GameObject menu;

 
    public void Pause(){
    	menu.SetActive(true);
    	//Time.timeScale = 0f;
    }

    public void GoToMenu(){
    	Time.timeScale = 1f;
    	SceneManager.LoadScene("MainMenu");
    }

    public void Restart(){
    	Time.timeScale = 1f;
    	SceneManager.LoadScene("BaseLevel");
    }
    public void Play(){
    	menu.SetActive(false);
    	Time.timeScale = 1f;
    }

}
