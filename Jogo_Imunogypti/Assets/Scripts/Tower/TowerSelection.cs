using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private TextMeshProUGUI headText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    public Ground selected;

    public static TowerSelection instance; //Classe estática

	void Awake()
    {
		if(instance!=null)
        {
			Debug.LogError("Mais de um TowerSelecrion");
			return;
		}

		instance = this;
	}

    void Update()
    {
        if(selected == null)
            return;
        if(selected.tower.level == 8) {
            headText.text = selected.tower.name + "(Nível: MAX)";
            upgradeText.text = "$ ----" ;
        }
        else {
            headText.text = selected.tower.name + "(Nível: " + selected.tower.level.ToString() + ")";
            upgradeText.text = "$ " + selected.tower.upgradeCost.ToString();
        }
        rangeText.text = "Alcance: " + selected.tower.GetRange().ToString();
        sellText.text = "$ " + (Shopping.instance.SalePrice(selected.tower)).ToString();
    }
    
    public void Select(Ground g)
    {
        if(g == selected)
        {    
            Hide();
            return;
        }

        if(selected != null)
            selected.tower.rangeCircle.SetActive(false);
        
        selected = g;
        selected.tower.rangeCircle.SetActive(true);
        Show();
        
    }

    public void Show()
    {
        selected.tower.expandRangeCircle();
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
        if(selected != null && selected.tower != null)
            selected.tower.rangeCircle.SetActive(false);
        selected = null;
    }

    public void UninstallSelected()
    {
        selected.Uninstall();
        Hide();
    }
}
