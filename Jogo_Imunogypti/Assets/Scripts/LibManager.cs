using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LibManager : MonoBehaviour {
    
    public TextMeshProUGUI TextBox;

    public void LibTextUpdate(string fileName) {
        string filePath = Application.streamingAssetsPath + "/Texts/" + fileName;
        Debug.Log(filePath);
        if(File.Exists(filePath)) {
            TextBox.text = File.ReadAllText(filePath);      
        }
    }
}
