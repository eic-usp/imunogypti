﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa os inimigos que devem ser derrotados
public class Virus : MonoBehaviour
{
    [SerializeField] private float notHP; //vida do inimigo(virus nao eh ser vivo, entao nao tem vida)
    [SerializeField] private float speed = 1; //velocidade com que o inimigo caminha pelo mapa
    [SerializeField] private int damage; //dano que o inimigo da ao jogador quando chega ao fim do caminho
    //[SerializeField] private Color color; // cor/sprite do inimigo
    private Transform target; //Dita a direção do movimento do virus
    private int wavePointIndex=0; //É adicionada de 1 a cada target alcançado

    void Start()
    {
        //Alvo inicial é o primeiro waypoint 
        target = Waypoints.points[0];
        //Se o waypoint for nulo
        if(target==null)
        		Debug.Log("Error: Target null");
    }

    void Update()
    {
    	// a Direção é o vetor que liga o virus ao target
        Vector3 dir = -this.transform.position + target.transform.position;
        //this.transform.LookAt(Camera.main.transform.position);
        //Move o virus na direção com uma velocidade 5f em relação ao World
        this.transform.Translate(dir.normalized * 5f * Time.deltaTime,Space.World);

        //Se a distancia entre o virus e o target é muito pequena, chame o método pra mudar de target
        if(Vector3.Distance(transform.position,target.position)<=0.2f){
        	GetNextWayPoint();
        }
    }

    //funcao que pega o ponto para onde o virus deve ir
    void GetNextWayPoint(){
    	//Se o virus está prestes a sair do ultimo target, destrua
    	if(wavePointIndex>=Waypoints.points.Length-1){
    		NotDeath();
    	}
    	//Senão, acrescente 1 ao index e mude de target
    	else{
    			wavePointIndex++;
    			target = Waypoints.points[wavePointIndex];
    	}
    }

    //funcao que da dano na (nao) vida do inimigo
    public void DealDamage(float damage)
    {
        notHP -= damage;
        
        //mata o  inimigo quando a (nao) vida chega a 0
        if(notHP <= 0)
            NotDeath();
    }

    //funcao que mata o inimigo quando a (nao) vida chega a 0 (virus nao eh ser vivo, entao nao morre)
    public void NotDeath()
    {
        
        Destroy(this.gameObject);
    }
}
