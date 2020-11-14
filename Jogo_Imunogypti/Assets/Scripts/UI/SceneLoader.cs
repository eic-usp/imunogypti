using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    public void PlayGame() {
        SceneManager.LoadScene("StageMenu");
        //SaveLoader.LoadGame();
    }

    public void ReturnToMenu() {
        FindObjectOfType<AudioManager>().Pause("Stage1");
        FindObjectOfType<AudioManager>().Play("MainTheme");
        LoadScene(0);
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(int i)//i é o numero da fase
    {
        if(i <= 1 || SaveLoader.saveFile.stagesWon[i-1])
            StartCoroutine(LoadLevel(i+1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
