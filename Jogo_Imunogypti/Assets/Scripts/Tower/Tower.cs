using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa o basico de todas as torres
public class Tower : MonoBehaviour
{
    public string name;
    public int level = 1;
    public bool active; //define se a torre esta ativa
    public int upgradeCost;
    public int cost;

   	public List<GameObject> targets;

    //Interfaces
   	private ITarget myTarget;
   	private IRotate myRotate;
   	private IEffect myEffect;


    //desenha o circulo do alcance da torre
    public GameObject rangeCircle;
    public bool drawRange = false;
    // public Tower[] towers;


    [SerializeField] protected TextAsset costTable;
    protected DynamicTable table;
    public DynamicTable Table 
    {
        get 
        {
            if(table == null)
                table = DynamicTable.Create(costTable);
            return table;
        }
    }

    void Awake()
    {
        table = DynamicTable.Create(costTable);
        cost = Table.Rows[0].Field<int>("Cost");
        upgradeCost = Table.Rows[1].Field<int>("Cost");
        name = Table.Rows[0].Field<string>("Name");

        myTarget = GetComponent<ITarget>();
        myRotate = GetComponent<IRotate>();
        myEffect = GetComponent<IEffect>();


        // rangeCircle = this.gameObject.transform.GetChild(2).gameObject;
        // rangeCircle.transform.localScale *=(myTarget.GetRange()+2);
    }

    void Update()
    {   
        if(active==false)
            return;

        if(this.gameObject.tag!="NaturalKiller"){
		  targets = myTarget.UpdateTarget(targets);
        }
        else if(!active){
            targets = myTarget.UpdateTarget(targets);
        }
        // Debug.Log("tagets: " + targets.Count);

    	//Rotaciona torre para olhar na direção do inimigo
        if(targets.Count != 0)
        {
            myRotate.LookAt(targets[0].transform, transform);
        }

        // Debug.Log("tagets1: " + targets.Count);
        myEffect.Apply(targets);
        // Debug.Log("tagets2: " + targets.Count);
        // if(drawRange == true){
        //     rangeCircle.SetActive(true);
        // }
        // else{
        //     rangeCircle.SetActive(false);
        // }
    }

    // private void OnMouseDown()
    // {
    //     towers = FindObjectsOfType(typeof(Tower)) as Tower[];
    //     foreach (Tower t in towers )
    //     {
    //         if(!t.Equals(this))
    //             t.drawRange = false;
    //     }
    //     Debug.Log("This is sparta");
    //     drawRange = !drawRange;
    //     EvolvePanel.instance.showEvolvePanel(this,drawRange);
    // }

    //deixa a torre ativa
    public void Activate()
    {
        active = true;
    }

    //deixa a torre desativada
    public void Deactivate()
    {
        active = false;
        myEffect.Remove(targets);
    }

    //desinstala a torre e remove seus efeitos
    public void Uninstall()
    {  
        myEffect.Remove(targets);
        Destroy(this.gameObject);
    }

    //evolui a torre
    public void Upgrade()
    {
        cost += upgradeCost;
        myTarget.Upgrade(level);
        myEffect.Upgrade(level);
        upgradeCost = Table.Rows[++level].Field<int>("Cost");
        
    }

    public bool CanUpgrade()
    {
        return level != 8;
    }

    public void Reset()
    {
        level = 1;
        cost = Table.Rows[0].Field<int>("Cost");
        upgradeCost = Table.Rows[1].Field<int>("Cost");
        myEffect.Remove(targets);
        myTarget.Reset();
    }

    public void expandRangeCircle(){
        rangeCircle.transform.localScale = new Vector3(1,1,1);
        rangeCircle.transform.localScale *=(myTarget.GetRange()+2);
    }

    public float GetRange()
    {
        //Debug.Log(myTarget.GetRange());
        return myTarget.GetRange();

    }

    public float GetAtk()
    {
        return myTarget.GetRange();

    }
}