using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

//classe que controla as principais funcoes do jogo
public class GameMannager : MonoBehaviour
{
    [SerializeField] private Base[] bases;
    [SerializeField] private int hp;
    [SerializeField] private int hpIni;
    [SerializeField] private Text hpText;
    public GameObject EndScreen;

    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private int wavesRemaning = 5; //marca quanntas ondas de inimigos faltam para o jogador vencer
    [SerializeField] private int waveNumber = 0; //marca qual a onda que o jogador esta enfrentando atualmente
    [SerializeField] private float timeBetweenWaves = 5f; //tempo entre as ondas de inimigos
    [SerializeField] private float countdown = 7f; //marca tempo ate a proxima onda de ininmigos chegar
    public int activeViruses = 0; //marca quantos virus ainda estao ativos
    [SerializeField] private Virus[] virusPrefab;
    public static GameMannager instance; //Classe estática
    [SerializeField] private Text wavesRemaningText;
    [SerializeField] private Text timeRemaningNextWaveText;

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um GameMannager");
            return;
        }

        instance = this;
    }

    void Start()
    {
        //jogo comeca na onda de numero 0, com nenhum virus ativo
        waveNumber = 0;
        activeViruses = 0;

        hpIni = hp;
    }

    void Update()
    {
        hpText.text = hp.ToString(); 
        wavesRemaningText.text = wavesRemaning.ToString(); 
        timeRemaningNextWaveText.text = Mathf.FloorToInt(countdown).ToString(); 

        //condicao de vitoria
        if(wavesRemaning <= 0 && activeViruses <= 0)
        {
            Win();
        }
        
        //verifica se esta na hora de mandar a proxima onda de inimigos
        if(countdown <= 0 && wavesRemaning > 0)
        {
            foreach (SpawnPoint sp in spawnPoints){
                StartCoroutine(sp.SpawnWave());
                // StartCoroutine(SpawnWave(hordes[waveNumber]));
            }
            
            countdown = timeBetweenWaves+waveNumber;
            wavesRemaning--;
            waveNumber++;
        }    
        else
            countdown -= Time.deltaTime; //conta tempo para mandar proxima onda de inimigos
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
            Defeat();
    }

    public void Defeat()
    {
        Debug.Log("PErDi");
        EndScreen.SetActive(true);
        EndScreen.transform.GetChild(1).gameObject.SetActive(true);
    }

    //funcao de vitoria do jogador
    public void Win()
    {
        Debug.Log("voce venceu, PARABAINS!!!");
        EndScreen.SetActive(true);
        EndScreen.transform.GetChild(0).gameObject.SetActive(true);
    }

    public int getHP(){
        return hp;
    }
    
    public int getIHP(){
        return hpIni;
    }
    
    public void CallNextWave()
    {
        countdown = 0;
    }

}
