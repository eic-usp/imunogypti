using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetDiscretDamage : MonoBehaviour, IEffect
{
    [SerializeField] private GameObject bulletPrefab;
    private float fireCountdown = 0f; 

    public void Apply(Transform firePoint, float attackSpeed, List<GameObject> targets, float damage){
        if(targets.Count != 0 && fireCountdown <= 0)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if(bullet != null)
            {
                bullet.target = targets[0];
                bullet.damage = damage;
            }   
            
            fireCountdown = 1f / attackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

}
