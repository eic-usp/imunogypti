using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofilo : Tower
{
    //Transforms relevantes 
   	[SerializeField] private Transform partToRotate;
   	[SerializeField] private Transform firepoint;
   	[SerializeField] private GameObject bulletPrefab;
   	private GameObject target;


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
       attackSpeed = 0.5f;
       damage = 3f;
    }

    void Update()
    {
		target = myTarget.UpdateTarget(range);
    	if(target==null)
    		return;

    	//Rotaciona torre para olhar na direção do inimigo
        partToRotate.rotation = myRotate.LookAt(target.transform, transform);

        myAttack.Shoot(bulletPrefab, firepoint, attackSpeed, target, damage);
    }
}
