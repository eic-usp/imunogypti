using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Virus virus = other.gameObject.transform.parent.GetComponent<Virus>();
    
        GameMannager.instance.TakeDamage(virus.damage);
    }

    public void BaseLocate(Vector3 locate)
    {
        transform.position = locate;
    }
}
