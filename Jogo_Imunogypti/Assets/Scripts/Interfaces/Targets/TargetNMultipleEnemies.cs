using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNMultipleEnemies : BaseTarget
{
	public override List<GameObject> UpdateTarget(List<GameObject> targets)
   	{
    	float distanceToEnemy;

		foreach(GameObject enemy in targets)
		{
			distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			
			//Se o inimigo sair do alcance ele não é mais um alvo
			if(distanceToEnemy>range)
				targets.Remove(enemy);
		}

		if(targets.Count == maxTargets)
			return targets;

   		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

		//Percorre todos os inimigos, tomando as ditancia da torre até eles e verificando se estão no alcance
		foreach(GameObject enemy in enemies)
		{
			if(targets.Count == maxTargets)
				break;

			bool cont = false;
			foreach(GameObject t in targets)
			{
				if(t == enemy)
				{
					cont = true;
					break;
				}
			}

			if(!cont)
			{
				distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
				
				//Se o inimigo não for nulo e a distancia estiver no range da torre, ele será um alvo
				if(distanceToEnemy<=range)
					targets.Add(enemy);
			}
		}

		return targets;
    }
}
