using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    
}

public interface ITarget
{
    string Tag {get;}
    Transform UpdateTarget(float range);
}

public interface IEffect
{

}

public interface IRotate
{
    Quaternion LookAt(Transform target, Transform tower);
}