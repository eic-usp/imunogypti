using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNMultipleEnemiesForAttack : BaseTarget
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
			//se atingir o maximo de alvos para de buscar alvos
			if(targets.Count == maxTargets)
				break;

			//verifica se o inimigo já está na lista de alvos
			bool cont = false;
			foreach(GameObject t in targets)
			{
				if(t == enemy)
				{
					cont = true;
					break;
				}
			}
			
			var virus = enemy.GetComponent<Virus>();

			//caso o inimigo já não esteja na lista de alvos
			if(!cont && !virus.stop)
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
