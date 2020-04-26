using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    DynamicTable Table {get;}
    void Upgrade(int level);
    List<GameObject> UpdateTarget();
    float GetRange();
}

public interface IEffect
{
    void Apply(List<GameObject> targets);
    void Remove(List<GameObject> targets);
}

public interface IRotate
{
    void LookAt(Transform target, Transform tower);
}