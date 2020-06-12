using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DistanceTower
{
    public GameObject enemy;
    public float distance;

    public DistanceTower(GameObject e, float d)
    {
        enemy = e;
        distance = d;
    }

    public static bool operator <(DistanceTower a, DistanceTower b)
    {
        if(a.distance < b.distance)
            return true;
        else
            return false;
    }

    public static bool operator >(DistanceTower a, DistanceTower b)
    {
        if(a.distance > b.distance)
            return true;
        else
            return false;
    }
}

public class DistanceQueue : MonoBehaviour
{
    private GameObject tower;
    private List<DistanceTower> data;

    public DistanceQueue(GameObject t) 
    {
        tower = t;
        data = new List<DistanceTower>();
    }

    //insere um Virus na fila em ordem da distancia dele da torre
    public void Enqueue(GameObject enemy){
        DistanceTower element = new DistanceTower(enemy, Vector3.Distance(tower.transform.position, enemy.transform.position));

        int i;
        for(i = 0; i < data.Count && element > data[i]; i++);

        data.Insert(i, element);
    }

    public GameObject Dequeue(){
        GameObject enemy = data[0].enemy;
        data.RemoveAt(0);
        return enemy;
    }

    public void Clear()
    {
        data.Clear();
    }
}