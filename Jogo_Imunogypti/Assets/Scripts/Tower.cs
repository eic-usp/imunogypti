using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa o basico de todas as torres
public class Tower : MonoBehaviour
{
    [SerializeField] protected bool active = false; //define se a torre esta ativa
    [SerializeField] protected float range = 5f; //alcance da torre, ex: do ataque
    [SerializeField] protected float attackSpeed = 5f; //velocidade de ataque da torre
    [SerializeField] protected float damage = 5f; //dano da torre
    public int cost;

    //Transforms relevantes 
   	[SerializeField] private Transform partToRotate;
   	[SerializeField] private Transform firepoint;
   	private List<GameObject> targets;

    //Interfaces
   	private ITarget myTarget;
   	private IRotate myRotate;
   	private IEffect myEffect;

	void Awake()
    {
        myTarget = GetComponent<ITarget>();
        myRotate = GetComponent<IRotate>();
        myEffect = GetComponent<IEffect>();
    }

    void Update()
    {   
        if(active==false)
            return;

		targets = myTarget.UpdateTarget(range);

    	//Rotaciona torre para olhar na direção do inimigo
        if(targets.Count != 0)
            partToRotate.rotation = myRotate.LookAt(targets[0].transform, transform);

        myEffect.Apply(firepoint, attackSpeed, targets, damage);
    }

    public void Activate()
    {
        active = true;
    }
}
