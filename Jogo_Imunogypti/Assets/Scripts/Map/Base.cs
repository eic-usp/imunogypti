using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    // public void OnTriggerEnter(Collider other) 
    // {
    //     Debug.Log("Ceehegoooou");
    //     Virus virus = other.gameObject.transform.parent.GetComponent<Virus>();
    
    //     Debug.Log("Chamo take damge na base");
    //     LifeManager.instance.TakeDamage(virus.damage);
    //     Destroy(other.gameObject);
    // }

    public void BaseLocate(Vector3 locate)
    {
        transform.position = locate;
    }
}
