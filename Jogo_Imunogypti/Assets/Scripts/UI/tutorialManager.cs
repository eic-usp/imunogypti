using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour {
    private int current;
    public GameObject[] scenes;
    public GameObject button = null;
    public GameObject block = null;
    public Shopping shop = null;
    public int levelNumber;

    // Start is called before the first frame update
    void Start() {
        current = 0;        
    }

    void Update() {
        if(button != null && shop.getGold() == 0) {
            button.SetActive(true);
        }
        if(current == 6 && shop.getGold() > 300 && levelNumber == 1 && block != null) {
            block.SetActive(false);
            loadNext();
        }
    }

    public void loadNext() {
        scenes[current].SetActive(false);
        current++;
        scenes[current].SetActive(true);
    }

}
