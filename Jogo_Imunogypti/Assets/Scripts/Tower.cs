using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa o basico de todas as torres
public class Tower : MonoBehaviour
{
    // [SerializeField] 
    protected float range = 5f; //alcance da torre, ex: do ataque
    // [SerializeField] 
    protected float attackSpeed = 5f; //velocidade de ataque da torre
    // [SerializeField] 
    protected float damage = 5f; //dano da torre

    // Tower()
    // {
    //     //this.gameObject.Instantiate(this.gameObject,new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,0)));
    // }
}
