using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LibManager : MonoBehaviour {
    
    public TextMeshProUGUI TextTitle;
    public Text TextBody;
    public LibTexts[] texts;

    public void LibTextUpdate(int i) {
        TextTitle.text = texts[i].text;
        //TextBody.text = texts[i].texts;
    }
}
