﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public abstract class BaseTarget : MonoBehaviour, ITarget
{
    [SerializeField] protected string tag;
    [SerializeField] protected float range;
    // public string Tag {get; private set;}   
    // public float Range {get; private set;}   
	protected List<GameObject> target = new List<GameObject>();
	[SerializeField] protected TextAsset targetTable;
    protected DynamicTable table;
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
        tag = Table.Rows[0].Field<string>("Tag");
		range = Table.Rows[0].Field<float>("Range");
        Debug.Log("tag: "+tag);
    }

    public void Upgrade(int level)
    {
        range = Table.Rows[level-1].Field<float>("Range");
    }

    public abstract List<GameObject> UpdateTarget();
}
