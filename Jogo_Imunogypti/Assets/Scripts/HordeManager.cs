using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class HordeManager:MonoBehaviour
{
    [SerializeField] private int wavesRemaning; //marca quanntas ondas de inimigos faltam para o jogador vencer
    [SerializeField] private int waveNumber; //marca qual a onda que o jogador esta enfrentando atualmente
    [SerializeField] private float timeBetweenWaves = 5f; //tempo entre as ondas de inimigos
    [SerializeField] private float countdown = 7f; //marca tempo ate a proxima onda de ininmigos chegar
    [SerializeField] private int totalSpawnPoints; //marca quantos SpawnPoint tera na tela
    [SerializeField] private int[] hordeComposition; //marca quais os inimigos que viram na onda atual
    public int activeViruses = 0; //marca quantos virus ainda estao ativos
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private Virus[] virusPrefab;
    [SerializeField] private Text wavesRemaningText;
    [SerializeField] private Text timeRemaningNextWaveText;

    public static HordeManager instance;

    [SerializeField] private TextAsset hordeTable;
    private DynamicTable table;
    public DynamicTable Table {
        get {
            if(table == null)
                table = DynamicTable.Create(hordeTable);
            return table;
        }
    }


    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um HordeManager");
            return;
        }

        instance = this;

        table = DynamicTable.Create(hordeTable);
        //seta o num de hordas
        wavesRemaning = Table.Rows[0].Field<int>("TotalWaves");
        //seta o num de SpawnPoint
        totalSpawnPoints = Table.Rows[0].Field<int>("TotalSpawnPoints");
    }

    void Start()
    {
        //jogo comeca na onda de numero 0, com nenhum virus ativo
        waveNumber = 0;
        activeViruses = 0;
    }

    void Update()
    {
        wavesRemaningText.text = wavesRemaning.ToString(); 
        timeRemaningNextWaveText.text = Mathf.FloorToInt(countdown).ToString(); 

        //condicao de vitoria
        if(wavesRemaning <= 0 && activeViruses <= 0)
            Win();

        //verifica se esta na hora de mandar a proxima onda de inimigos
        if(countdown <= 0 && wavesRemaning > 0)
        {
            for(int i = 0; i < totalSpawnPoints; i++)
            {
                string nSP;
                nSP = "SP" + i.ToString();
                
                string[] s_hordeComposition = Table.Rows[waveNumber].Field<string>(nSP).Split(',');
                hordeComposition = Array.ConvertAll(s_hordeComposition, s => int.Parse(s));

                StartCoroutine(SpawnWave(i));
            }
                countdown = timeBetweenWaves+waveNumber;
                wavesRemaning--;
                waveNumber++;
        }    

        if(countdown > 0)
            countdown -= Time.deltaTime; //conta tempo para mandar proxima onda de inimigos
    }


    //funcao que libera uma onda de inimigos
    IEnumerator SpawnWave(int spawnPoint)
    {
        activeViruses += hordeComposition.Length; //coloca todos os inimigos que serao estanciados nessa onda como ativos

        //estancia os inimigos dessa onda
        foreach(int virus in hordeComposition)
        {
            virusPrefab[virus].spawnPoint = spawnPoints[spawnPoint];
            Vector3 rotation = new Vector3(-90f ,0.4f,0f);
            Instantiate(virusPrefab[virus], spawnPoints[spawnPoint].gameObject.transform.position, Quaternion.Euler(rotation));
            yield return new WaitForSeconds(0.5f); //tempo entre a instanciacao de cada inimigo
        }
    }

    public void CallNextWave()
    {
        countdown = 0;
    }

    public void Win()
    {
        LifeManager.instance.Win();
    }
}