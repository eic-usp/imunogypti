using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Classe estática que faz a compra das torres
public class Shopping : MonoBehaviour
{
    private float price;
    [SerializeField] private int gold; //dinheiro do jogador
    [SerializeField] private Text goldText; //mostrar pro jogador quanto dinheiro ele tem
    private float returnPercentageGold = 0.5f;
    public static Shopping instance; //Classe estática
    [SerializeField] public Tower[] sales; //Torres que serão vendidas
    [SerializeField] private TextMeshProUGUI[] priceText;

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um Shopping");
            return;
        }

        instance = this;
    }
    
    void Update()
    {
        goldText.text = gold.ToString();

        for(int i=0;i<sales.Length;i++){
            priceText[i].text = "$" + sales[i].cost.ToString();
        }
    }

    //funcao que adiciona dinheiro ao jogador
    public void EarnGold(int amount)
    {
        //jogador nao pode ganhar dinheiro negativo
        if(amount < 0)
            return;

        gold += amount;
    }

    //funcao que retira dinheiro do jogador quando ele gasta
    public bool ShellOut(int amount)
    {
        //jogador não pode gastar um dinheiro que ele nao tem
        if(amount > gold)
            return false;

        gold -= amount;
        return true;
    }

    public int SalePrice(Tower tower)
    {
        return (int)(tower.cost*returnPercentageGold);
    }

    public void Sell()
    {
        EarnGold(SalePrice(TowerSelection.instance.selected.tower));
        TowerSelection.instance.UninstallSelected();
    }

    public void Upgrade()
    {
        if(TowerSelection.instance.selected.tower.CanUpgrade() && ShellOut(TowerSelection.instance.selected.tower.upgradeCost))
            TowerSelection.instance.selected.tower.Upgrade();
    }

    public int getGold() {
        return gold;
    }
}
