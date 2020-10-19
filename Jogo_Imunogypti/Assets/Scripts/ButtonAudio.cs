using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour {
    private AudioManager audioM;

    void Start() {
        audioM = FindObjectOfType<AudioManager>();
    }

    public void Play(string name) {
        if(audioM == null) Start();
        audioM.Play(name);
    }

    public void Pause(string name) {
        audioM.Pause(name);
    }

}
