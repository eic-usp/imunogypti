using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNoAttack : MonoBehaviour, IEffect
{
    public void Upgrade(int level){}
    public void Downgrade(){}
    public abstract void Apply(List<GameObject> targets);
    public abstract void Remove(List<GameObject> targets);
}