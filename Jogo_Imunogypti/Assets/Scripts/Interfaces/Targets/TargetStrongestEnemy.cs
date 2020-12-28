using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Foca o inimigo mais forte(com mais vida)
public class TargetStrongestEnemy : BaseTarget
{
	private float distanceToEnemy;

    public override List<GameObject> UpdateTarget(List<GameObject> targets)
   	{
   		if(target.Count != 0 && target[0] != null)
		{
			Virus virus = target[0].GetComponent<Virus>();
			if(virus.hp > 0) {//verifica se o virus ainda esta vivo
				distanceToEnemy = Vector3.Distance(transform.position, target[0].transform.position);
				if(distanceToEnemy <= range)
					return target;
			}
		}

		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
    	//Força do inimigo mais forte
    	float strongest = 0;
    	GameObject strongestEnemy = null;
		target.Clear();

    	//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo o inimigo mais forte
    	foreach(GameObject enemy in enemies)
		{
    		distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			Virus virus = enemy.GetComponent<Virus>();

    		if(!virus.stop && virus.hp > strongest && distanceToEnemy <= range)
			{
    			strongest = virus.hp;
    			strongestEnemy = enemy;
    		}
    	}

    	//Se o inimigo mais proximo não for nulo e a distancia estiver no range da torre, ele será o alvo
    	if(strongestEnemy!=null)
			target.Add(strongestEnemy);
	
		return target;
    }
}
