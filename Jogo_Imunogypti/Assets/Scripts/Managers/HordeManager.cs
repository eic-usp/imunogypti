using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;

public class HordeManager:MonoBehaviour
{
    [SerializeField] private int wavesRemaning; //marca quanntas ondas de inimigos faltam para o jogador vencer
    private int totalWaves;
    public int waveNumber; //marca qual a onda que o jogador esta enfrentando atualmente
    [SerializeField] private float timeBetweenWaves = 10f; //tempo entre as ondas de inimigos
    [SerializeField] private float countdown = 7f; //marca tempo ate a proxima onda de ininmigos chegar
    [SerializeField] private int totalSpawnPoints; //marca quantos SpawnPoint tera na tela
    // [SerializeField] private int[] hordeComposition; //marca quais os inimigos que viram na onda atual
    // [SerializeField] private string[] hordeComposition; //marca quais os inimigos que viram na onda atual
    public int activeViruses = 0; //marca quantos virus ainda estao ativos
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private Virus[] virusPrefab;
    [SerializeField] private TextMeshProUGUI wavesRemaningText;
    [SerializeField] private TextMeshProUGUI timeRemaningNextWaveText;

    [SerializeField] private TextMeshProUGUI ImmunityRewardText;
    [SerializeField] private float ImmunityReward;
    [SerializeField] private float maxImmunityReward;
    private bool ShowImmunityReward = false;

    [SerializeField] private bool AutomaticMode=true;
    [SerializeField] private bool WaveCalled=false;

    [SerializeField] private float endValue=0;
    [SerializeField] private float timeForDecrease=10;
    [SerializeField] private float elapsedTime=10;

    //[SerializeField] private float timeRewardDecrease = 1.75f;
    //[SerializeField] private float RewardCountdown = 1.75f;

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
        totalWaves = Table.Rows[0].Field<int>("TotalWaves");
        //seta o num de SpawnPoint
        totalSpawnPoints = Table.Rows[0].Field<int>("TotalSpawnPoints");
        countdown = timeBetweenWaves;
    }

    void Start()
    {
        //jogo comeca na onda de numero 0, com nenhum virus ativo
        waveNumber = 0;
        activeViruses = 0;
    }

    void Update()
    {
        wavesRemaningText.text = waveNumber.ToString() + "/" + totalWaves.ToString(); 
        if(wavesRemaning > 0) {
            timeRemaningNextWaveText.text = Mathf.FloorToInt(countdown).ToString(); 
        }else {
            timeRemaningNextWaveText.text = "fim";
        }

        //condicao de vitoria
        if(wavesRemaning <= 0 && activeViruses <= 0)
            Win();

        //verifica se esta na hora de mandar a proxima onda de inimigos
        if(wavesRemaning > 0)
        {
            if(countdown<=0 && AutomaticMode ==true){
                CallWave();
                ShowImmunityReward = true;
                countdown = timeBetweenWaves;
                //timeRewardDecrease = 0.25f*countdown;
                wavesRemaning--;
                waveNumber++;
            }

            if(AutomaticMode==false && WaveCalled ==true ) {
                CallWave();
                wavesRemaning --;
                waveNumber++;
                WaveCalled = false;
            }    
        }    

        if(countdown > 0 && AutomaticMode == true)
            countdown -= Time.deltaTime; //conta tempo para mandar proxima onda de inimigos
        else
            countdown = timeBetweenWaves;

        if(elapsedTime<timeForDecrease){
            ImmunityReward = Mathf.Lerp(maxImmunityReward,0,(elapsedTime/timeForDecrease));
            //ImmunityRewardText.text = ((int)ImmunityReward).ToString();
            elapsedTime+=Time.deltaTime;
            //Debug.Log(elapsedTime);
        }
        
    }

    //funcao que libera uma onda de inimigos
    IEnumerator SpawnWave(int spawnPoint, string[] hordeComposition)
    {
        ImmunityReward = 0;
        
        float timeBetweenViruses;
        for(int i = 0; i*3 < hordeComposition.Length; i++)
        {
            int nType = int.Parse(hordeComposition[i*3]);
            int Type = int.Parse(hordeComposition[(i*3)+1]);
            activeViruses += nType; //coloca todos os inimigos que serao estanciados nessa onda como ativos
            timeBetweenViruses = float.Parse(hordeComposition[(i*3)+2])/100;
            Debug.Log("horde: " + waveNumber + " wait: " + timeBetweenViruses);
            for(int j = 0; j < nType; j++)
            {
                virusPrefab[Type].spawnPoint = spawnPoints[spawnPoint];
                Instantiate(virusPrefab[Type], spawnPoints[spawnPoint].gameObject.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(timeBetweenViruses); //tempo entre a instanciacao de cada inimigo
            }
        }

        //ImmunityReward = 0;
        //ImmunityRewardText.text = ImmunityReward.ToString();
        //estancia os inimigos dessa onda
        // foreach(int virus in hordeComposition)
        // {
        //     virusPrefab[virus].spawnPoint = spawnPoints[spawnPoint];
        //     // Vector3 rotation = new Vector3(-90f ,0.4f,0f);
        //     Instantiate(virusPrefab[virus], spawnPoints[spawnPoint].gameObject.transform.position, Quaternion.identity);
        //     yield return new WaitForSeconds(0.5f); //tempo entre a instanciacao de cada inimigo
        // }

        elapsedTime = 0;
        maxImmunityReward = Table.Rows[waveNumber-1].Field<float>("ImmunityReward");
    }

    public void CallNextWave()
    {
        if(AutomaticMode == true)
            countdown = 0;
        else
            WaveCalled = true;

        ImmunityManager.instance.Increase(ImmunityReward);
        ImmunityReward = 0;
        //ImmunityRewardText.text = ((int)ImmunityReward).ToString();
        elapsedTime = 10f;
    }

    public void Win()
    {
        LifeManager.instance.Win();
    }

    public void MultiplyVirus(Virus virus)
    {
        activeViruses ++; //coloca o inimigo que sera estanciado como ativo
        // Vector3 rotation = new Vector3(-90f ,0.4f,0f);
        // SpawnVirus(spawnPoints[spawnPoint].gameObject.transform.position, Quaternion.identity);
        var v = Instantiate(virus);
        v.invader = true;
    }

    public void CallWave()
    {
        for(int i = 0; i < totalSpawnPoints; i++)
        {
            string nSP;
            nSP = "SP" + i.ToString();
            
            string[] hordeComposition = Table.Rows[waveNumber].Field<string>(nSP).Split(',');
            //hordeComposition = Array.ConvertAll(s_hordeComposition, s => int.Parse(s));

            StartCoroutine(SpawnWave(i, hordeComposition));
        }
    }

    public void ChangeAutomaticMode()
    {
        if(AutomaticMode==true)
            AutomaticMode = false;
        else
            AutomaticMode = true;
    }

    public int getTotalSpawnPoints()
    {
        return totalSpawnPoints;
    }

    public SpawnPoint GetSpawnPoint(int number)
    {
        return spawnPoints[number];
    }
/*
    IEnumerator DecreaseReward(){

        //maxImmunityReward = Table.Rows[waveNumber-1].Field<float>("ImmunityReward");
        ImmunityReward = 100f;
        ImmunityRewardText.text = ImmunityReward.ToString();

        float elapsedTime = 0f;
        float timeForDecrease = 10f;

        while(elapsedTime<timeForDecrease){
            ImmunityReward = Mathf.Lerp(ImmunityReward,0,(elapsedTime/timeForDecrease));

            ImmunityRewardText.text = ((int)ImmunityReward).ToString();
            elapsedTime+=2*Time.deltaTime;

            yield return null;

        }
        ShowImmunityReward = false;
        yield return null;
    }
    */
}