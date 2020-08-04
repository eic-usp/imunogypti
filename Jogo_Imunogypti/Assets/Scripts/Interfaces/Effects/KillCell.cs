using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCell : MonoBehaviour, IEffect
{

    public void Apply(List<GameObject> targets)
    {
        if(targets.Count <= 0)
            return;

        Debug.Log("not null");
        var cell = targets[0].GetComponent<MultipliesViruses>();
        cell.Destroyed();
        Destroy(this.gameObject);
    }

    public void Remove(List<GameObject> targets){}
}
