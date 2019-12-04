using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofilo : MonoBehaviour
{

    public static float price = 300f;
    private Transform target;
    public float range  =5f;

    public string enemyTag = "Enemy";

   	public Transform partToRotate;

   	public Renderer rendPartToRotate;

   void Start()
    {
       InvokeRepeating("UpdateTarget",0f,0.5f);
       rendPartToRotate.sortingLayerName = "Layer1";
    }

   	void UpdateTarget()
   	{
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    	float shortestDistance = Mathf.Infinity;
    	GameObject nearestEnemy = null;

    	foreach(GameObject enemy in enemies){
    		float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    		if(distanceToEnemy<shortestDistance){
    			shortestDistance = distanceToEnemy;
    			nearestEnemy = enemy;
    		}
    	}
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
    	Vector3 dir = target.position - transform.position;
    	Quaternion lookRotation = Quaternion.LookRotation(dir);
    	Vector3 rotation = lookRotation.eulerAngles;

        partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y,0f);
    }


    public float getPrice(){
    	return price;
    }


}
