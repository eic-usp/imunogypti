using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//classe que controla as principais funcoes do jogo
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int wavesRemaning = 5; //marca quanntas ondas de inimigos faltam para o jogador vencer
    [SerializeField] private int waveNumber = 0; //marca qual a onda que o jogador esta enfrentando atualmente
    // public List<List<Virus> > hordes; //guarda os inimigos que serao liberados para enfrentar o jogador
    private List<List<Virus> > hordes = new List<List<Virus> >(); //guarda os inimigos que serao liberados para enfrentar o jogador
    [SerializeField] private float timeBetweenWaves = 5f; //tempo entre as ondas de inimigos
    [SerializeField] private float countdown = 2f; //marca tempo ate a proxima onda de ininmigos chegar
    public int activeViruses = 0; //marca quantos virus ainda estao ativos
    [SerializeField] private Virus virusPrefab;
    public static SpawnPoint instance; //Classe estática

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um SpawnPoint");
            return;
        }

        instance = this;
    }

    void Start()
    {
        //jogo comeca na onda de numero 0, com nenhum virus ativo
        waveNumber = 0;
        activeViruses = 0;

        //seta as ondas de inimigos
        for(int i = 0; i < wavesRemaning; i++)
        {
            List<Virus> aux = new List<Virus>();
            for(int j = i*i + 5; j > 0; j--)
            {
                aux.Add(virusPrefab);
            }
            hordes.Add(aux);
        }
    }

    void Update()
    {
        //condicao de vitoria
        if(wavesRemaning <= 0 && activeViruses <= 0)
        {
            Win();
        }
        
        //verifica se esta na hora de mandar a proxima onda de inimigos
        if(countdown <= 0 && wavesRemaning > 0)
        {
            //StartCoroutine(SpawnWave(hordes[waveNumber]));
            StartCoroutine(SpawnWave(hordes[waveNumber]));
            // StartCoroutine(SpawnWave());
            wavesRemaning--;
            waveNumber++;
            countdown = timeBetweenWaves;
        }    

        countdown -= Time.deltaTime; //conta tempo para mandar proxima onda de inimigos
    }

    //funcao que libera uma onda de inimigos
    IEnumerator SpawnWave(List<Virus> horde)
    // IEnumerator SpawnWave()
    {
        activeViruses += horde.Count; //coloca todos os inimigos que serao estanciados nessa onda como ativos

        //estancia os inimigos dessa onda
        foreach (Virus virus in horde)
        {
            Instantiate(virus, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f); //tempo entre a instanciacao de cada inimigo
        }
    }

    //funcao de vitoria do jogador
    public void Win()
    {
        Debug.Log("voce venceu, PARABAINS!!!");
        SceneManager.LoadScene("Win");
    }
}

