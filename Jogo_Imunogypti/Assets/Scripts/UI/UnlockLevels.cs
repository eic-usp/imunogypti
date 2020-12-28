using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevels : MonoBehaviour {
    public Button[] levels;

    // Start is called before the first frame update
    void Start() {
        levels[0].interactable = true;
        for(int i = 0; i < 7; i++) {
            if(SaveLoader.saveFile.stagesWon[i]) {
                levels[i+1].interactable = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
