using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damage;
    public int Cost { get; private set; }

}
