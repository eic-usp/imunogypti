using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : BaseAttack
{
    //[SerializeField] private float damage=15f;
    [SerializeField] private float explosionRange=5f;

    public override void Apply(List<GameObject> targets)
    {
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    	foreach(GameObject enemy in enemies){
    		float distanceToTarget = Vector3.Distance(targets[0].transform.position, enemy.transform.position);
    		if(distanceToTarget<explosionRange){
    			Virus virus = enemy.GetComponent<Virus>();
    			virus.DealDamage(damage);
    		}
    	}

    	if(targets.Count>0){
    		Destroy(this.gameObject);
    	}

    }
}
