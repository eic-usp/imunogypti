using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour, IEffect
{
    
    [SerializeField] protected float attackSpeed; //velocidade de ataque da torre
    [SerializeField] protected float damage; //dano da torre

    //Tabela contendo dano e velocidade de ataque
	[SerializeField] protected TextAsset attackTable;
    protected DynamicTable table;
    public DynamicTable Table {
        get {
            if(table == null)
                table = DynamicTable.Create(attackTable);
            return table;
        }
    }

    protected void Awake()
    {
        table = DynamicTable.Create(attackTable);
        damage = Table.Rows[0].Field<int>("Damage");
		attackSpeed = Table.Rows[0].Field<float>("AttackSpeed");
    }

    public void Upgrade(int level)
    {
        damage = Table.Rows[level-1].Field<float>("Damage");
        attackSpeed = Table.Rows[level-1].Field<int>("AttackSpeed");
    }

    public void Downgrade()
    {
        damage = Table.Rows[0].Field<int>("Damage");
		attackSpeed = Table.Rows[0].Field<float>("AttackSpeed");
	}

    public abstract void Apply(List<GameObject> targets);

    public void Remove(List<GameObject> targets){}
}