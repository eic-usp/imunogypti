using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Text headText;
    [SerializeField] private Text rangeText;
    [SerializeField] private Text upgradeText;
    [SerializeField] private Text sellText;
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

        headText.text = selected.tower.name + "(nível: " + selected.tower.level.ToString() + ")";
        rangeText.text = "alcance: " + selected.tower.GetRange().ToString();
        upgradeText.text = "evoluir: $" + selected.tower.upgradeCost.ToString();
        sellText.text = "vender: $" + (Shopping.instance.SalePrice(selected.tower)).ToString();
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
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
        if(selected.tower != null)
            selected.tower.rangeCircle.SetActive(false);
        selected = null;
    }

    public void UninstallSelected()
    {
        selected.Uninstall();
        Hide();
    }
}
