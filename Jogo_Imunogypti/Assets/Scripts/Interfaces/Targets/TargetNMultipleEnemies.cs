using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNMultipleEnemies : BaseTarget
{
	public override List<GameObject> UpdateTarget()
   	{
   		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
    	//Distancia mais curta até um inimigo
    	float distanceToEnemy;
		target.Clear();

		//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo a menor distancia e portanto o inimigo mais proximo
		foreach(GameObject enemy in enemies)
		{
			if(target.Count == maxTargets)
				break;

			distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			
			//Se o inimigo não for nulo e a distancia estiver no range da torre, ele será um alvo
			if(distanceToEnemy<=range)
				target.Add(enemy);
		}

		return target;
    }
}
