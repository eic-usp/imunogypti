﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMacrofagoInGround : BaseNoAttack
{
    [SerializeField] private float buffAttackSpeed; //porcentagem que a velocidade de ataque da torre esta sendo buffada
    [SerializeField] private float buffDamage; //porcentagem que o dano da torre esta sendo buffado

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
}
