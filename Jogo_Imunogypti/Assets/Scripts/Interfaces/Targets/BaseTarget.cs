using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public abstract class BaseTarget : MonoBehaviour, ITarget
{
    [SerializeField] protected string tag;
    [SerializeField] protected float range;
    [SerializeField] protected int maxTargets;
	protected List<GameObject> target = new List<GameObject>();

    //Tabela contendo alcance e tag
	[SerializeField] protected TextAsset targetTable;
    protected DynamicTable table;
    public DynamicTable Table {
        get {
            if(table == null)
                table = DynamicTable.Create(targetTable);
            return table;
        }
    }

    protected void Awake()
    {
        table = DynamicTable.Create(targetTable);
        tag = Table.Rows[0].Field<string>("Tag");
		range = Table.Rows[0].Field<float>("Range")/10;
		maxTargets = Table.Rows[0].Field<int>("MaxTargets");
    }

    public void Upgrade(int level)
    {
        range = Table.Rows[level].Field<float>("Range")/10;
        maxTargets = Table.Rows[level].Field<int>("MaxTargets");
    }

    public void Reset()
    {
        tag = Table.Rows[0].Field<string>("Tag");
		range = Table.Rows[0].Field<float>("Range")/10;
		maxTargets = Table.Rows[0].Field<int>("MaxTargets");
    }


    public abstract List<GameObject> UpdateTarget(List<GameObject> targets);

    public float GetRange()
    {
        return range;
    }
}
