using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    public void PlayGame()
    {
        SceneManager.LoadScene("StageMenu");
        // SaveLoader.LoadGame();
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(int i)
    {
        StartCoroutine(LoadLevel(i));
        /*if(i == 1 || SaveLoader.saveFile.stagesWon[i-1])
            StartCoroutine(LoadLevel(i));*/
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
