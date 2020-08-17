using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPath : BaseTarget
{
    private float distanceToPath;
    private float previousDistance;
    public override List<GameObject> UpdateTarget(List<GameObject> targets)
   	{
   		GameObject[] paths = GameObject.FindGameObjectsWithTag(tag);

   		List<GameObject> nearestPath = new List<GameObject>();

   		foreach(GameObject path in paths)
		{
			previousDistance = distanceToPath;
    		distanceToPath = Vector3.Distance(transform.position, path.transform.position);

    		if(distanceToPath <= range){
				if(distanceToPath<previousDistance)
					nearestPath.Add(path);
    		}
    	}

		return nearestPath;

   	}
    
}
