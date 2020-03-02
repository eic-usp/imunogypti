using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMultipleEnemies : BaseTarget
{
    public override List<GameObject> UpdateTarget()
   	{
   		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tag);
    	//Distancia mais curta até um inimigo
    	float distanceToEnemy;
		target.Clear();

    	//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo a menor distancia e portanto o inimigo mais proximo
    	foreach(GameObject enemy in enemies)
		{
    		distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			
			//Se o inimigo mais proximo não for nulo e a distancia estiver no range da torre, ele será o alvo
			if(distanceToEnemy<=Range)
				target.Add(enemy);
    	}

		return target;
    }
}
