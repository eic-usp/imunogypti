using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa os inimigos que devem ser derrotados
public class Virus : MonoBehaviour
{
    [SerializeField] private float notHP; //vida do inimigo(virus nao eh ser vivo, entao nao tem vida)
    [SerializeField] private float speed = 1; //velocidade com que o inimigo caminha pelo mapa
    [SerializeField] private int damage; //dano que o inimigo da ao jogador quando chega ao fim do caminho
    //[SerializeField] private Color color; // cor/sprite do inimigo
    [SerializeField] private Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(speed, 0f, 0f); 
    }

    //funcao que da dano na (nao) vida do inimigo
    public void DealDamage(float damage)
    {
        notHP -= damage;
        
        //mata o  inimigo quando a (nao) vida chega a 0
        if(notHP <= 0)
            NotDeath();
    }

    //funcao que mata o inimigo quando a (nao) vida chega a 0 (virus nao eh ser vivo, entao nao morre)
    public void NotDeath()
    {
        Destroy(this.gameObject);
    }
}
