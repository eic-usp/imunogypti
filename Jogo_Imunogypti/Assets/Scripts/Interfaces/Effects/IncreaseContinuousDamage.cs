using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseContinuousDamage : MonoBehaviour, IEffect
{
   	[SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] protected float multiplierIncreaseRate; //velocidade de ataque da torre
    [SerializeField] protected float initialDamage; //dano da torre
    private GameObject previousTarget = null;
    [SerializeField] private float multiplier = 1;
    private Animator myAnimator;

    public void Apply(List<GameObject> targets){
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
        enemy.DealDamage(initialDamage*multiplier);

        multiplier += Time.deltaTime*multiplierIncreaseRate;

        previousTarget = targets[0];
    }

    public void Remove(List<GameObject> targets){}

    public void SetMultiplier(float x){
        initialDamage = x;
    }
}
