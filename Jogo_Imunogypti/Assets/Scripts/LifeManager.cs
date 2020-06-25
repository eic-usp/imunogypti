using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

//classe que controla as principais funcoes do jogo
public class LifeManager : MonoBehaviour
{
    [SerializeField] private Base[] bases;
    [SerializeField] private int hp;
    [SerializeField] private int hpIni;
    [SerializeField] private Text hpText;
    public GameObject EndScreen;
    public static LifeManager instance; //Classe estática

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um LifeManager");
            return;
        }

        instance = this;
    }

    void Start()
    {
        hpIni = hp;
    }

    void Update()
    {
        hpText.text = hp.ToString();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
            Defeat();
    }

    //funcao de derrota do jogador
    public void Defeat()
    {
        Debug.Log("perdeu");
        EndScreen.SetActive(true);
        EndScreen.transform.GetChild(1).gameObject.SetActive(true);
    }

    //funcao de vitoria do jogador
    public void Win()
    {
        Debug.Log("ganhou");
        EndScreen.SetActive(true);
        EndScreen.transform.GetChild(0).gameObject.SetActive(true);
    }

    public int getHP(){
        return hp;
    }
    
    public int getIHP(){
        return hpIni;
    }
}
