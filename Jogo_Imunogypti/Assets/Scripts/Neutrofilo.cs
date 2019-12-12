using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofilo : Tower
{
    //Transforms relevantes 
   	[SerializeField] private Transform partToRotate;
   	private Transform target;

   	private ITarget myTarget;
   	private IRotate myRotate;

	void Awake()
    {
        myTarget = GetComponent<ITarget>();
        myRotate = GetComponent<IRotate>();
    }

	void Start()
    {
       //InvokeRepeating("myTarget.UpdateTarget",0f,0.5f);
    }

    void Update()
    {
		target = myTarget.UpdateTarget(range);
    	if(target==null)
    		return;

    	//Rotaciona torre para olhar na direção do inimigo
        partToRotate.rotation = myRotate.LookAt(target, transform);
    }
}
