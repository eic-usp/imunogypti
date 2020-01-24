using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLinfocitosTCD8 : MonoBehaviour, ITarget
{
    public string Tag {get; set;}
	private float distanceToLinfocito;

    void Awake()
    {
        Tag = "LinfocitoTCD8";
    }

    public List<GameObject> UpdateTarget(float range)
   	{
   		GameObject[] linfocitos = GameObject.FindGameObjectsWithTag(Tag);
    	
    	List<GameObject> linfocitosInRange = new List<GameObject>();

    	//Percorre todos os inimigos, tomando as ditancia da torre até eles e decidindo o inimigo mais forte
    	foreach(GameObject linfocito in linfocitos)
		{
    		distanceToLinfocito = Vector3.Distance(transform.position, linfocito.transform.position);

    		if(distanceToLinfocito <= range)
			{
				linfocitosInRange.Add(linfocito);
    		}
    	}

		return linfocitosInRange;
    }
}
