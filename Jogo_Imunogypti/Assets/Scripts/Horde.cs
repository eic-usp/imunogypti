using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horde{
    private int hordeIndex;
    private List<Virus> wave = new List<Virus>();
     public List<Virus> genotype = new List<Virus>();
    private float SpawnDelay=0f;
    private int level;
    HordeManager instanceManager = HordeManager.instance;


    public Horde(int id, int lvl, int[] composition){
        //Pega a lista de genetipos
        genotype = instanceManager.genotypeList;

        if(composition==null){
            return;
        }

        //Atribui id, nivel e delay da horda
        hordeIndex = id;
        level = lvl;


        //leitura do vetor na tabela e construção da horda
        for(int j=0;j<composition.Length;j++){
            for(int k=0;k<composition[j];k++){
                wave.Add(genotype[j]);
                SpawnDelay+=genotype[j].getSpawnTime();
            }
        }

      

    }

    public IEnumerator SpawnWave(GameObject spawnPoint){

        //instancia os inimigos com um tempo calculado entre cada um
        Debug.Log("tchau");
        foreach(Virus gen in wave){
            Vector3 rotation = new Vector3(-90f,0.4f,0);
            GameObject.Instantiate(gen.gameObject, spawnPoint.transform.position, Quaternion.Euler(rotation));
            yield return new WaitForSeconds(0.3f*gen.getSpawnTime()); //tempo entre a instanciacao de cada inimigo
        }
    }

    public int getActiveViruses(){ // Tempo entre dois virus consecutivos por tipo por horda
        
        return wave.Count;
    }


    public float getSpawnDelay(){
        return SpawnDelay;
    }
}

