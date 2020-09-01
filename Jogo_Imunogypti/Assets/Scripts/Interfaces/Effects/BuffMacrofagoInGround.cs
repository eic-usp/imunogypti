using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMacrofagoInGround : BaseNoAttack
{
    [SerializeField] private float buffAttackSpeed; //porcentagem que a velocidade de ataque da torre esta sendo buffada
    [SerializeField] private float buffDamage; //porcentagem que o dano da torre esta sendo buffado

    //Tabela contendo buff de dano e velocidade de ataque
	[SerializeField] protected TextAsset buffTable;
    protected DynamicTable table;
    public DynamicTable Table {
        get {
            if(table == null)
                table = DynamicTable.Create(buffTable);
            return table;
        }
    }

    protected void Awake()
    {
        table = DynamicTable.Create(buffTable);
        //estao em porcentagem, por isso divide por 100
        buffDamage = Table.Rows[0].Field<float>("Damage")/100;
        buffAttackSpeed = Table.Rows[0].Field<float>("AttackSpeed")/100;
    }

    public override void Apply(List<GameObject> targets){
        foreach (GameObject ground in targets)
        {
            Ground activeGround = ground.GetComponent<Ground>();
            activeGround.ActivateBuffMacrofago(buffAttackSpeed, buffDamage);
        }
    }

    public override void Remove(List<GameObject> targets)
    {
        foreach (GameObject ground in targets)
        {
            Ground activeGround = ground.GetComponent<Ground>();
            activeGround.DeactivateBuffMacrofago();
        }
    }

    public override void Upgrade(int level)
    {
        //estao em porcentagem, por isso divide por 100
        buffDamage = Table.Rows[level].Field<float>("Damage")/100;
        buffAttackSpeed = Table.Rows[level].Field<float>("AttackSpeed")/100;
    }
}
