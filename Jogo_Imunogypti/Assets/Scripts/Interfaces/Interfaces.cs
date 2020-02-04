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
    void Apply(List<GameObject> targets);
}

public interface IRotate
{
    void LookAt(Transform target, Transform tower);
}