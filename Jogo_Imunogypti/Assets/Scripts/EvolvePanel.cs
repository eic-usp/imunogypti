using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EvolvePanel : MonoBehaviour
{
        public Image evolvePanel; // Painel de evolução de torres
        public Text Area;
        public Text Dano;
        public Text Head;
        public Text Price;
        public static EvolvePanel instance;
    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um EvolvePanel");
            return;
        }

        instance = this;
    }
    void Start()
    {
        evolvePanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showEvolvePanel(Tower t,bool draw){
        evolvePanel.gameObject.SetActive(draw);
        Area.text = "Alcance: "+t.getRange().ToString();
        Head.text = t.gameObject.tag +" "+ t.getID();
        Price.text = "$"+(t.cost*2).ToString();
        
    }
}
