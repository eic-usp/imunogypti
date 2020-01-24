using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLinfocitosTCD8 : MonoBehaviour, IEffect
{
    public void Apply(Transform firePoint, float attackSpeed, List<GameObject> targets, float damage){
        foreach (GameObject Linfocito in targets)
        {
            Tower tower = Linfocito.GetComponent<Tower>();
            tower.Activate();
        }
    }
}
