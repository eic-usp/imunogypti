using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Classe estática que faz a compra das torres
public class Shopping : MonoBehaviour
{
    private float price;
    [SerializeField] private int gold; //dinheiro do jogador
    public static Shopping instance; //Classe estática

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um Shopping");
            return;
        }

        instance = this;
    }

    void Start()
    {
        gold = 400; //dinheiro inicial do jogador
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
        Debug.Log("Dinehiro: " + gold);
        return true;
    }
}
