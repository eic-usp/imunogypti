using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscretDamage : MonoBehaviour, IEffect
{
   	[SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackSpeed; //velocidade de ataque da torre
    private float buffAttackSpeed = 1; //porcentagem que a velocidade de ataque da torre esta sendo buffada
    [SerializeField] private float damage; //dano da torre
    private float buffDamage = 1; //porcentagem que o dano da torre esta sendo buffado
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
                    bullet.damage = damage * buffDamage;
                }   
            }
            
            fireCountdown = 1f / (attackSpeed * buffAttackSpeed);
        }

        fireCountdown -= Time.deltaTime;
    }

    public void Remove(List<GameObject> targets){}

    public void Buff(float buffAS, float buffD)
    {
        buffAttackSpeed += buffAS;
        buffDamage += buffD;
    }
}
