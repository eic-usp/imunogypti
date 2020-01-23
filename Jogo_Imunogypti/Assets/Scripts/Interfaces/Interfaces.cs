using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void Shoot(Transform firePoint, float attackSpeed, GameObject target, float damage);
}

public interface ITarget
{
    string Tag {get;}
    GameObject UpdateTarget(float range);
}

public interface IEffect
{

}

public interface IRotate
{
    Quaternion LookAt(Transform target, Transform tower);
}