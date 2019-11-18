using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] private float notHP;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    //[SerializeField] private Color color;
    [SerializeField] private Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(speed, 0f, 0f);
    }

    public void DealDamage(float damage)
    {
        notHP -= damage;
        
        if(notHP <= 0)
            NotDeath();
    }

    public void NotDeath()
    {
        Destroy(this.gameObject);
    }
}
