using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Creditos : MonoBehaviour
{
    public Image creditsButton;
    public Text creditsBText;
    public Image statisticsButton;
    public Text statisticsBText;
    public Image CreditsPanel;
    public Image StatisticsPanel;

    void Start()
    {
        creditsButton.color = new Color32(108,59,59,255);
        creditsBText.color = new Color32(239,239,239,255);
        statisticsButton.color = new Color32(226,226,226,255);
        statisticsBText.color = new Color32(120,120,120,255);
       	StatisticsPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectCredits(){
    	creditsButton.color = new Color32(108,59,59,255);
        creditsBText.color = new Color32(239,239,239,255);
        statisticsButton.color = new Color32(226,226,226,255);
        statisticsBText.color = new Color32(120,120,120,255);
        CreditsPanel.gameObject.SetActive(true);
        StatisticsPanel.gameObject.SetActive(false);
    }
    public void SelectStatistics(){
    	statisticsButton.color = new Color32(108,59,59,255);
    	statisticsBText.color = new Color32(239,239,239,255);
    	creditsButton.color = new Color32(226,226,226,255);
    	creditsBText.color = new Color32(120,120,120,255);
    	CreditsPanel.gameObject.SetActive(false);
    	StatisticsPanel.gameObject.SetActive(true);
    }

    public void ReturnToMainMenu(){
    	SceneManager.LoadScene("MainMenu");
    }
}
