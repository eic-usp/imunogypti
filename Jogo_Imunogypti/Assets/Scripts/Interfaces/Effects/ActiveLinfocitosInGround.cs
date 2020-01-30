using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLinfocitosInGround : MonoBehaviour, IEffect
{
    public void Apply(Transform firePoint, float attackSpeed, List<GameObject> targets, float damage){
        foreach (GameObject ground in targets)
        {
            Ground activeGround = ground.GetComponent<Ground>();
            activeGround.Activate();
        }
    }
}
