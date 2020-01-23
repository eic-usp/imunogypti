using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinfocitoTCD8 : Tower
{
    //Transforms relevantes 
   	[SerializeField] private Transform partToRotate;
   	[SerializeField] private Transform firepoint;
   	private GameObject target;
    private float multiplier;
    [SerializeField] private LineRenderer lineRenderer;

   	private ITarget myTarget;
   	private IRotate myRotate;
   	private IAttack myAttack;

	void Awake()
    {
        myTarget = GetComponent<ITarget>();
        myRotate = GetComponent<IRotate>();
        myAttack = GetComponent<IAttack>();
    }

	void Start()
    {
       //InvokeRepeating("myTarget.UpdateTarget",0f,0.5f);
    }

    void Update()
    {
        target = myTarget.UpdateTarget(range);

    	//Rotaciona torre para olhar na direção do inimigo
        if(target!=null) partToRotate.rotation = myRotate.LookAt(target.transform, transform);
        myAttack.Shoot(null, firepoint, 0f, target, damage);
    }
}
