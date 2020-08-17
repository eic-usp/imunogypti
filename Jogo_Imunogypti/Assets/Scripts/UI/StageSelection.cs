using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    [SerializeField] private List<string> scenes;

    public void LoadStage(int stage)
    {
        SceneManager.LoadScene(scenes[stage]);
    }
}
