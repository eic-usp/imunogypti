using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLinfocitosInGround : BaseNoAttack
{
    public override void Apply(List<GameObject> targets){
        foreach (GameObject ground in targets)
        {
            Ground activeGround = ground.GetComponent<Ground>();
            activeGround.Activate();
        }
    }

    public override void Remove(List<GameObject> targets)
    {
        foreach (GameObject ground in targets)
        {
            Ground activeGround = ground.GetComponent<Ground>();
            activeGround.Deactivate();
        }
    }
}
