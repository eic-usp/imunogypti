using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa o basico de todas as torres
public class Tower : MonoBehaviour
{
    private int level = 1;
    public bool active = false; //define se a torre esta ativa
    public int upgradeCost;
    public int cost;

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

		targets = myTarget.UpdateTarget();

    	//Rotaciona torre para olhar na direção do inimigo
        if(targets.Count != 0)
            myRotate.LookAt(targets[0].transform, transform);

        myEffect.Apply(targets);
    }

    public void Activate()
    {
        active = true;
    }
    
    public void Deactivate()
    {
        active = false;
    }

    public void Uninstall()
    {  
        myEffect.Remove(targets);
        Destroy(this.gameObject);
    }

    public void Upgrade()
    {
        myTarget.Upgrade(++level);
    }
}
