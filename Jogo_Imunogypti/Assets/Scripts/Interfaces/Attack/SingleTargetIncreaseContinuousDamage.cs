using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetIncreaseContinuousDamage : MonoBehaviour, IAttack
{
    [SerializeField] private LineRenderer lineRenderer;
    private GameObject previousTarget = null;
    private float multiplier = 1;

    public void Shoot(Transform firePoint, float attackSpeed, GameObject target, float damage){
        if(previousTarget != target)
            multiplier = 1;

        if(target != null)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.transform.position);

            Virus enemy = target.GetComponent<Virus>();
            enemy.DealDamage(damage*multiplier);

            multiplier += Time.deltaTime;
        }
        else if(lineRenderer.enabled)
            lineRenderer.enabled = false;

        previousTarget = target;
    }

}
