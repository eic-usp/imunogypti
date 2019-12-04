using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
	//Dita a direção do movimento do virus
    private Transform target;
    //É adicionada de 1 a cada target alcançado
    private int wavePointIndex=0;

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
    void GetNextWayPoint(){
    	//Se o virus está prestes a sair do ultimo target, destrua
    	if(wavePointIndex>=Waypoints.points.Length-1){
    		Destroy(gameObject);
    	}
    	//Senão, acrescente 1 ao index e mude de target
    	else{
    			wavePointIndex++;
    			target = Waypoints.points[wavePointIndex];
    	}
    }
}
