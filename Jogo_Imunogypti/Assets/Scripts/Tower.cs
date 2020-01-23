using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa o basico de todas as torres
public class Tower : MonoBehaviour
{
    [SerializeField] protected float range = 5f; //alcance da torre, ex: do ataque
    [SerializeField] protected float attackSpeed = 5f; //velocidade de ataque da torre
    [SerializeField] protected float damage = 5f; //dano da torre
    public int cost;

    //Transforms relevantes 
   	[SerializeField] private Transform partToRotate;
   	[SerializeField] private Transform firepoint;
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

    void Update()
    {
		target = myTarget.UpdateTarget(range);

    	//Rotaciona torre para olhar na direção do inimigo
        if(target!=null)
            partToRotate.rotation = myRotate.LookAt(target.transform, transform);

        myAttack.Shoot(firepoint, attackSpeed, target, damage);
    }
}
