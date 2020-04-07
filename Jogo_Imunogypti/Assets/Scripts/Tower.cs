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

    [SerializeField] protected TextAsset costTable;
    protected DynamicTable table;
    public DynamicTable Table {
        get {
            if(table == null)
                table = DynamicTable.Create(costTable);
            return table;
        }
    }

    protected void Awake()
    {
        table = DynamicTable.Create(costTable);
        upgradeCost = Table.Rows[0].Field<int>("Cost");

        myTarget = GetComponent<ITarget>();
        myRotate = GetComponent<IRotate>();
        myEffect = GetComponent<IEffect>();
    }

    void Update()
    {   
        if(active==false)
            return;

		targets = myTarget.UpdateTarget();
        // Debug.Log("tagets: " + targets.Count);

    	//Rotaciona torre para olhar na direção do inimigo
        if(targets.Count != 0)
            myRotate.LookAt(targets[0].transform, transform);

        // Debug.Log("tagets1: " + targets.Count);
        myEffect.Apply(targets);
        // Debug.Log("tagets2: " + targets.Count);
    }

    public void Activate()
    {
        active = true;
    }
    
    public void Deactivate()
    {
        active = false;
        myEffect.Remove(targets);
    }

    public void Uninstall()
    {  
        myEffect.Remove(targets);
        Destroy(this.gameObject);
    }

    public void Upgrade()
    {
        cost += upgradeCost;
        upgradeCost = Table.Rows[level].Field<int>("Cost");
        myTarget.Upgrade(++level);
    }
}
