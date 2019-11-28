using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe que controla as principais funcoes do jogo
public class MainManager : MonoBehaviour
{
    [SerializeField] private int wavesRemaning = 1; //marca quanntas ondas de inimigos faltam para o jogador vencer
    [SerializeField] private int waveNumber = 0; //marca qual a onda que o jogador esta enfrentando atualmente
    [SerializeField] private List<List<Virus> > hordes; //guarda os inimigos que serao liberados para enfrentar o jogador
    [SerializeField] private float timeBetweenWaves = 5f; //tempo entre as ondas de inimigos
    [SerializeField] private float countdown = 2f; //marca tempo ate a proxima onda de ininmigos chegar
    public int activeViruses = 0; //marca quantos virus ainda estao ativos
    
    void Start()
    {
        //jogo comeca na onde de numero 0, com nenhum virus ativo
        waveNumber = 0;
        activeViruses = 0;
    }

    void Update()
    {
        //condicao de vitoria
        if(wavesRemaning <= 0 and activeViruses <= 0)
        {
            Win();
        }
        
        //verifica se esta na hora de mandar a proxima onda de inimigos
        if(countdown <= 0f and wavesRemaning > 0)
        {
            StartCoroutine(SpawnWave(hordes[wave]));
            wavesRemaning--;
            waveNumber++;
            countdown = countdownBetweenEnemysSpawn;
        }    

        countdown -= Time.deltaTime; //conta tempo para mandar proxima onda de inimigos
    }

    //funcao que libera uma onda de inimigos
    IEnumerator SpawnWave(List<Virus> horde)
    {
        activeViruses += horde.Count; //coloca todos os inimigos que serao estanciados nessa onda como ativos

        //estancia os inimigos dessa onda
        foreach (Virus virus in horde)
        {
            Instantiate(virus.Transform, Vector3.zero, Quaternion.identity);
            yield return new WaitForSeconds(0.3f); //tempo entre a instanciacao de cada inimigo
        }
    }

    //funcao de vitoria do jogador
    public void Win()
    {
        Debug.Log("voce venceu, PARABAINS!!!")
    }
}

