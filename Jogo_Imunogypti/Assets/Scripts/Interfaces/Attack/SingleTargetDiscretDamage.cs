using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetDiscretDamage : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject bulletPrefab;
    private float fireCountdown = 0f; 

    public void Shoot(Transform firePoint, float attackSpeed, GameObject target, float damage){
        if(target != null && fireCountdown <= 0)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.target = target;
                bullet.damage = damage;
            }   
            
            fireCountdown = 1f / attackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

}
