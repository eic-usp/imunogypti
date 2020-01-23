using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Foca o inimigo mais forte(com mais vida)
public class TargetStrogestEnemy : MonoBehaviour, ITarget
{
    public string Tag {get; set;}   
	private GameObject previousTarget = null;
	private float distanceToEnemy;

    void Awake()
    {
        Tag = "Enemy";
    }

    public GameObject UpdateTarget(float range)
   	{
   		if(previousTarget != null)
		{
			distanceToEnemy = Vector3.Distance(transform.position, previousTarget.transform.position);
			if(distanceToEnemy <= range)
				return previousTarget;
		}  

		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tag);
    	//Força do inimigo mais forte
    	float strongest = 0;
    	GameObject strongestEnemy = null;

    	//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo o inimigo mais forte
    	foreach(GameObject enemy in enemies)
		{
    		distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			Virus virus = enemy.GetComponent<Virus>();

    		if(virus.hp > strongest && distanceToEnemy <= range)
			{
    			strongest = virus.hp;
    			strongestEnemy = enemy;
    		}
    	}

		previousTarget = strongestEnemy;

    	//Se o inimigo mais proximo não for nulo e a distancia estiver no range da torre, ele será o alvo
    	if(strongestEnemy !=null)
			return strongestEnemy;
    	else
			return null;
    }
}
