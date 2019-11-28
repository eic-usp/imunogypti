using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sistema da compra e evolucao de torres do jogo 
public class BuildManager : MonoBehaviour
{
    //dinheiro do jogador
    [SerializeField] private int gold;

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
        if(amount > gold);
            return false;

        gold -= amount;
        return true;
    }

    //funcao que compra e instala torres no mapa
    public void Install(Tower tower)
    {
        //verifica se o jogador tem dinheiro o suficiente para fazer a compra
        if(!ShellOut(tower.Cost))
            return;

        gold -= tower.Cost;
        Instantiate(tower.gameObject, Vector3.zero, Quaternion.identity);
    }
}
