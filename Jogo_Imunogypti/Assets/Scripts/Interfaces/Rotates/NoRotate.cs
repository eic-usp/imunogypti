using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotate : MonoBehaviour, IRotate
{
    public Quaternion LookAt(Transform target, Transform tower)
    {
        return Quaternion.identity;
    }
}
