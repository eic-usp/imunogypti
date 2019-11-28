using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa o basico de todas as torres
public class Tower : MonoBehaviour
{
    [SerializeField] private float range; //alcance da torre, ex: do ataque
    [SerializeField] private float attackSpeed; //velocidade de ataque da torre
    [SerializeField] private float damage; //dano da torre
    public int Cost { get; private set; } //valor que a torre custa

}
