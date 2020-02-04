using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{
    private float timeScale = 1;

    public void ChangeRhythm()
    {
        Time.timeScale = timeScale = Time.timeScale%3 + 1;
    }

    public void ReturnRhythm()
    {
        Time.timeScale = timeScale;
    }
}
