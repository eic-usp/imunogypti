using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNearestEnemys : BaseTarget
{
    public override List<GameObject> UpdateTarget()
   	{
		DistanceQueue queue = new DistanceQueue(gameObject);
   		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
		foreach(GameObject enemy in enemies)
			queue.Enqueue(enemy);
		
		//Distancia mais curta até um inimigo
		target.Clear();
    	float shortestDistance;
    	GameObject nearestEnemy;

		for(int i = 0; i < maxTargets; i++){
			GameObject enemy = queue.Dequeue();

			//Se o inimigo mais proximo não for nulo e a distancia estiver no range da torre, ele será o alvo
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if(enemy!=null && distanceToEnemy<=range)
				target.Add(enemy);
		}

		return target;
    }
}
