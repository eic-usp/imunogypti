using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public abstract class BaseTarget : MonoBehaviour, ITarget
{
    public string Tag {get; private set;}   
    public float Range {get; private set;}   
	protected List<GameObject> target = new List<GameObject>();
	[SerializeField] private TextAsset targetTable;
    private DynamicTable table;
    public DynamicTable Table {
        get {
            if(table == null)
                table = DynamicTable.Create(targetTable);
            return table;
        }
    }

    void Awake()
    {
        table = DynamicTable.Create(targetTable);
        Tag = Table.Rows[0].Field<string>("Tag");
		Range = Table.Rows[0].Field<float>("Range");
    }

    public void Upgrade(int level)
    {
        Range = Table.Rows[level-1].Field<float>("Range");
    }

    public abstract List<GameObject> UpdateTarget();
}
