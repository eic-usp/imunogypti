using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipliesViruses : MonoBehaviour, IEffect
{
    [SerializeField] private bool destroyed = false;
    [SerializeField] private int waveToRespawn = 0;
    [SerializeField] private int nInvaders = 0;
    [SerializeField] private int progression = 0;
    private List<Virus> enemies = new List<Virus>();

    public void Apply(List<GameObject> targets)
    {
        if(destroyed)
        {
            if(HordeManager.instance.waveNumber == waveToRespawn)
            {
                destroyed = false;
                nInvaders = 0;
                progression = 0;
                enemies.Clear();
                targets.Clear();
            }
            return;
        }

        for(int i = nInvaders; i < targets.Count; i++)
        {
            var virus = targets[i].GetComponent<Virus>();

            if(!virus.invader)
            {
                enemies.Add(virus);
                enemies[i].InvadeCell(this.transform);
                nInvaders++;
            }
            else
            {
                targets.Remove(targets[i]);
            }
        }

        foreach (Virus enemy in enemies)
        {
            if(!enemy.stop && Vector3.Distance(transform.position,enemy.transform.position) <= 0.2f)
            {
                enemy.stop = true;
                progression++;
            }
        }

        if(progression == 10)
        {
            destroyed = true;
            StartCoroutine(Multiply());
        }
    }

    public void Remove(List<GameObject> targets)
    {

    }

    IEnumerator Multiply()
    {
        foreach (Virus enemy in enemies)
        {
            yield return new WaitForSeconds(0.1f); //tempo entre a instanciacao de cada inimigo
            HordeManager.instance.MultiplyVirus(enemy);
            yield return new WaitForSeconds(0.1f); //tempo entre a instanciacao de cada inimigo
            enemy.Evacuate();
        }

        waveToRespawn = HordeManager.instance.waveNumber + 1;
    }

    public void Destroyed()
    {
        destroyed = true;
        waveToRespawn = HordeManager.instance.waveNumber + 1;
        foreach (Virus enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        this.gameObject.GetComponent<Tower>().targets.Clear();
    }
}
