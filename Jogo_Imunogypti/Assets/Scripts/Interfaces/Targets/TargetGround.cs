using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGround : BaseTarget
{
	private float distanceToGround;

    public override List<GameObject> UpdateTarget(List<GameObject> targets)
   	{
   		GameObject[] grounds = GameObject.FindGameObjectsWithTag(tag);
    	
    	List<GameObject> groundsInRange = new List<GameObject>();

    	//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo o inimigo mais forte
    	foreach(GameObject ground in grounds)
		{
    		distanceToGround = Vector3.Distance(transform.position, ground.transform.position);

    		if(distanceToGround <= range)
				groundsInRange.Add(ground);
    	}

		return groundsInRange;
    }
}
