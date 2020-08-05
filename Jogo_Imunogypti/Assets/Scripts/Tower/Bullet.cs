using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float damage;
    [SerializeField] private float speed = 0.5f;

    void Update() {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }    

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }       

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void HitTarget()
    {
        Virus enemy = target.GetComponent<Virus>();
        enemy.DealDamage(damage);

        Destroy(gameObject);
    }
}
