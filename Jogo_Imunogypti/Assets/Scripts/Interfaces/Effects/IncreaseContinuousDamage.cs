using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseContinuousDamage : BaseAttack
{
   	[SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    // [SerializeField] protected float multiplierIncreaseRate; //velocidade de ataque da torre
    // [SerializeField] protected float initialDamage; //dano da torre
    private GameObject previousTarget = null;
    [SerializeField] private float multiplier = 1;
    [SerializeField] private float buffDamage = 1; //porcentagem que o dano da torre esta sendo buffado
    private Animator myAnimator;

    public override void Apply(List<GameObject> targets){
        myAnimator = GetComponent<Animator>();

        if(targets.Count == 0)
        {
            if(lineRenderer.enabled)
                lineRenderer.enabled = false;

            multiplier = 1;
            return;
        }
        if(myAnimator!=null)
            myAnimator.SetTrigger("Shoot");
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targets[0].transform.position);
        if(previousTarget != targets[0])
            multiplier = 1;

        Virus enemy = targets[0].GetComponent<Virus>();
        enemy.DealDamage(damage*multiplier*buffDamage);

        multiplier += Time.deltaTime*attackSpeed;

        previousTarget = targets[0];
    }

    public void Buff(float buffD)
    {
        buffDamage += buffD;
    }
}
