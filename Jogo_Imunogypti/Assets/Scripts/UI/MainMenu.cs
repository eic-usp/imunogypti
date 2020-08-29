using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("StageMenu");
        // SaveLoader.LoadGame();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int i)
    {
        if(i == 1 || SaveLoader.saveFile.stagesWon[i-1])
            SceneManager.LoadScene(i+3);
    }
}
