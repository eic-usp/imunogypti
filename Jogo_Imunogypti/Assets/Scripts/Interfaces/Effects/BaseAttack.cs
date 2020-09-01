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
        damage = Table.Rows[0].Field<float>("Damage");
		attackSpeed = Table.Rows[0].Field<float>("AttackSpeed")/100; //o valor esta multiplicado por 100 para nao colocar virgula/ponto
    }

    public void Upgrade(int level)
    {
        damage = Table.Rows[level].Field<float>("Damage");
        attackSpeed = Table.Rows[level].Field<float>("AttackSpeed")/100; //o valor esta multiplicado por 100 para nao colocar virgula/ponto
    }

    public abstract void Apply(List<GameObject> targets);

    public void Remove(List<GameObject> targets){}
}