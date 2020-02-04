using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscretDamage : MonoBehaviour, IEffect
{
   	[SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] protected float attackSpeed; //velocidade de ataque da torre
    [SerializeField] protected float damage; //dano da torre
    private float fireCountdown = 0f; 

    public void Apply(List<GameObject> targets){
        if(targets.Count != 0 && fireCountdown <= 0)
        {
            foreach (GameObject target in targets)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if(bullet != null)
                {
                    bullet.target = target;
                    bullet.damage = damage;
                }   
            }
            
            fireCountdown = 1f / attackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

}
