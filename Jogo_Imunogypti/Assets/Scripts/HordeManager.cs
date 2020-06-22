using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class HordeManager:MonoBehaviour{

    private List<Horde> hordes = new List<Horde>();
    public List<Virus> genotypeList = new List<Virus>();

    public GameObject SpawnPoint;

    public static HordeManager instance;

    private int totalCount;
    private int actualHorde;

    private float countdown=0;
    public int activeViruses;

    [SerializeField] private Text wavesRemaningText;
    [SerializeField] private Text timeRemaningNextWaveText;

    [SerializeField] protected TextAsset hordeTable;
    protected DynamicTable table;
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

        totalCount = 0;
        actualHorde = 0;
        
        table = DynamicTable.Create(hordeTable);

        for(int i=0;i<Table.Rows.Count;i++){

        	//Pega os parâmetros da horda da tabela
            int hordeID = Table.Rows[i].Field<int>("ID");
            int hordeLevel = Table.Rows[i].Field<int>("LEVEL");

            string[] s_hordeComposition = Table.Rows[i].Field<string>("COMPOSITION").Split(',');
            int[] hordeComposition = Array.ConvertAll(s_hordeComposition, s => int.Parse(s));

        	//Vê se a horda pertence ao nivel atual e pega as hordas do nivel atual
            if(hordeLevel==1){//Alterar mais tarde, para hordeLevel igual ao nivel atual.
                Horde horde = new Horde(hordeID, hordeLevel, hordeComposition);
                hordes.Add(horde);
                totalCount++;
            }
        }

    }


    void Update()
    {
        wavesRemaningText.text = (hordes.Count - actualHorde).ToString(); 
        timeRemaningNextWaveText.text = Mathf.FloorToInt(countdown).ToString(); 

        if(hordes.Count - actualHorde <= 0 && activeViruses <= 0)
        {
            Win();
        }

        //Verifica se o contador zerou e se a horda atual é a ultima
        if(countdown<=0 && actualHorde<hordes.Count){
            SpawnHorde();
            actualHorde++;
            Debug.Log(hordes.Count.ToString());
        }
        //Diminui o contador
        if(countdown>0){
            countdown -=Time.deltaTime;
        }
        

    }

    void SpawnHorde(){
                //Spawna a wave, e espera o tempo da horda.
           		//Debug.Log(hordes[actualHorde].getSpawnDelay().ToString());
                StartCoroutine(hordes[actualHorde].SpawnWave(SpawnPoint));
                countdown = hordes[actualHorde].getSpawnDelay();
                activeViruses += hordes[actualHorde].getActiveViruses();
    }

    public void CallNextWave()
    {
        countdown = 0;
    }


     public void Win()
    {
        Debug.Log("voce venceu, PARABAINS!!!");
        Base.instance.Won();
    }
}
