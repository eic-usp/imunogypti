using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetIncreaseContinuousDamage : MonoBehaviour, IEffect
{
    [SerializeField] private LineRenderer lineRenderer;
    private GameObject previousTarget = null;
    private float multiplier = 1;

    public void Apply(Transform firePoint, float attackSpeed, List<GameObject> targets, float damage){
        if(targets.Count == 0)
        {
            if(lineRenderer.enabled)
                lineRenderer.enabled = false;

            multiplier = 1;
            return;
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targets[0].transform.position);

        if(previousTarget != targets[0])
            multiplier = 1;

        Virus enemy = targets[0].GetComponent<Virus>();
        enemy.DealDamage(damage*multiplier);

        multiplier += Time.deltaTime;

        previousTarget = targets[0];
    }

}
