using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private int gold;

    void Start()
    {
        gold = 400;
    }

    public void EarnGold(int amount)
    {
        if(gold < 0)
            return;

        gold += amount;
    }

    public void ShellOut(int amount)
    {
        if(amount > gold);
            return;

        gold -= amount;
    }

    public void Install(Tower tower)
    {
        if(!CanBuild(tower))
            return;

        gold -= tower.Cost;
        Instantiate(tower.gameObject, Vector3.zero, Quaternion.identity);
    }

    public bool CanBuild(Tower tower)
    {
        if(tower.Cost > gold) 
            return true;

        return false;
    }

    public bool CanUpgrade(Tower tower)
    {
        return false;
    }
}
