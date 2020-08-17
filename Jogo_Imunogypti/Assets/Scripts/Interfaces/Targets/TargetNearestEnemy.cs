using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNearestEnemy : BaseTarget
{

    public override List<GameObject> UpdateTarget(List<GameObject> targets)
   	{
   		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
    	//Distancia mais curta até um inimigo
        float shortestDistance = Mathf.Infinity;
    	GameObject nearestEnemy = null;
		target.Clear();

    	//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo a menor distancia e portanto o inimigo mais proximo
    	foreach(GameObject enemy in enemies)
		{
    		float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			var virus = enemy.GetComponent<Virus>();
    		if(!virus.stop && distanceToEnemy<shortestDistance)
			{
    			shortestDistance = distanceToEnemy;
    			nearestEnemy = enemy;
    		}
    	}

    	//Se o inimigo mais proximo não for nulo e a distancia estiver no range da torre, ele será o alvo
    	if(nearestEnemy!=null && shortestDistance<=range)
			target.Add(nearestEnemy);
			
		return target;
    }
}
