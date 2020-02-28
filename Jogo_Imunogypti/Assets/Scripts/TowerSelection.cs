using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Text upgrade;
    [SerializeField] private Text sell;
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

        upgrade.text = "$" + selected.tower.upgradeCost.ToString();
        sell.text = "$" + (Shopping.instance.SalePrice(selected.tower)).ToString();
    }
    
    public void Select(Ground g)
    {
        if(g == selected)
        {    
            Hide();
            return;
        }

        selected = g;
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
        selected = null;
    }

    public void UninstallSelected()
    {
        selected.Uninstall();
        Hide();
    }
}
