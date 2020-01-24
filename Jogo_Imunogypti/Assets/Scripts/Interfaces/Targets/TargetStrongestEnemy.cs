using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Foca o inimigo mais forte(com mais vida)
public class TargetStrongestEnemy : MonoBehaviour, ITarget
{
    public string Tag {get; set;}
	private float distanceToEnemy;
	List<GameObject> target = new List<GameObject>();

    void Awake()
    {
        Tag = "Enemy";
    }

    public List<GameObject> UpdateTarget(float range)
   	{
   		if(target.Count != 0 && target[0] != null)
		{
			distanceToEnemy = Vector3.Distance(transform.position, target[0].transform.position);
			if(distanceToEnemy <= range)
				return target;
		}  

		//Os inimigos são todos com a tag de inimigos
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tag);
    	//Força do inimigo mais forte
    	float strongest = 0;
    	GameObject strongestEnemy = null;
		target.Clear();

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

    	//Se o inimigo mais proximo não for nulo e a distancia estiver no range da torre, ele será o alvo
    	if(strongestEnemy!=null)
			target.Add(strongestEnemy);
	
		return target;
    }
}
