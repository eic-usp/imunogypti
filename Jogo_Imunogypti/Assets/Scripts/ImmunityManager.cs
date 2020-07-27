using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityManager : MonoBehaviour
{
    [SerializeField] private int immunity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Increase(int amount){
    	immunity +=amount;
    }

    void Decrease(int amount){
    	immunity-=amount;
    }
}
