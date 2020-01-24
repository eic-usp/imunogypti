using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    string Tag {get;}
    List<GameObject> UpdateTarget(float range);
}

public interface IEffect
{
    void Apply(Transform firePoint, float attackSpeed, List<GameObject> targets, float damage);
}

public interface IRotate
{
    Quaternion LookAt(Transform target, Transform tower);
}