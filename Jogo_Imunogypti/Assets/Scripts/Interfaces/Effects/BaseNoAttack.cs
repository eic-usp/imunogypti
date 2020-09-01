using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNoAttack : MonoBehaviour, IEffect
{
    public virtual void Upgrade(int level){}
    public abstract void Apply(List<GameObject> targets);
    public abstract void Remove(List<GameObject> targets);
}