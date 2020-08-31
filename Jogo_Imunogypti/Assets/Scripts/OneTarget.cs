using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTarget : MonoBehaviour, IEffect
{
    [SerializeField] private float damage=15f;

    public void Apply(List<GameObject> targets)
    {
    	foreach(GameObject target in targets){
    		Virus virus = target.GetComponent<Virus>();
    		virus.DealDamage(damage);
    	}
    	if(targets.Count>0){
    		Destroy(this.gameObject);
    	}

    }

    public void Remove(List<GameObject> targets){}
	public void Upgrade(int level){}
    public void Downgrade(){}

}
