using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofilo : Tower
{
	//Variáveis com atributos da torre
    //public static float price = 300f;
    //public float range  =5f;

    //Tag dos alvos
    public string enemyTag = "Enemy";

    //Transforms relevantes 
   	public Transform partToRotate;
   	private Transform target;

   	//public Renderer rendPartToRotate;

	void Start()
    {
       InvokeRepeating("UpdateTarget",0f,0.5f);
    }

   	void UpdateTarget()
   	{
   		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    	//Distancia mais curta até um inimigo
    	float shortestDistance = Mathf.Infinity;
    	GameObject nearestEnemy = null;

    	//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo a menor distancia e portanto o inimigo mais proximo
    	foreach(GameObject enemy in enemies){
    		float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    		if(distanceToEnemy<shortestDistance){
    			shortestDistance = distanceToEnemy;
    			nearestEnemy = enemy;
    		}
    	}
    	//Se o inimigo mais proximo não for nulo e a distancia estiver no range da torre, ele será o alvo
    	if(nearestEnemy !=null && shortestDistance<=range){
    		target = nearestEnemy.transform;
    	}
    	else{
    		target = null;
    	}
    }

    void Update()
    {
    	if(target==null){
    		return;
    	}
    	//A direção entre a torre e o inimigo
    	Vector3 dir = target.position - transform.position;
    	//Quaternion com rotação para fazer a torre olhar pro inimigo
    	Quaternion lookRotation = Quaternion.LookRotation(dir);
    	Vector3 rotation = lookRotation.eulerAngles;


    	//Rotaciona torre para olhar na direção do inimigo
        partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y,0f);
    }


}
