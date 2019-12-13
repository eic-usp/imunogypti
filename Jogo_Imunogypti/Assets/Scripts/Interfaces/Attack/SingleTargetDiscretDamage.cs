using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetDiscretDamage : MonoBehaviour, IAttack
{
    float fireCountdown = 0f; 

    public void Shoot(GameObject bulletPrefab, Transform firePoint, float attackSpeed, GameObject target, float damage){
        if(fireCountdown <= 0)
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
