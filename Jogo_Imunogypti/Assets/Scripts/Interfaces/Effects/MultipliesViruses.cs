using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipliesViruses : BaseNoAttack
{
    [SerializeField] private bool destroyed = false;
    [SerializeField] private int waveToRespawn = 0;
    [SerializeField] private int progression = 0;
    private List<Virus> enemies = new List<Virus>();
    private List<Color> StandardColors = new List<Color>();//Lista com cores padrão da torre
    Animator Anim; //Animator da celula

    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        // var t = this.gameObject.GetComponent<Tower>();
        foreach(Renderer r in this.gameObject.GetComponentsInChildren<Renderer>())
            StandardColors.Add(r.material.color);
    }

    public override void Apply(List<GameObject> targets)
    {
        //se a célula estiver destruída
        if(destroyed)
        {
            //se a célula já deve ser reparada
            if(HordeManager.instance.waveNumber == waveToRespawn)
            {
                Anim.SetTrigger("Revive");
                changeTurretColor(Color.white);
                destroyed = false;
                progression = 0;
                enemies.Clear();
                targets.Clear();
            }
            return;
        }

        for(int i = enemies.Count; i < targets.Count; i++)
        {
            var virus = targets[i].GetComponent<Virus>();

            if(!virus.invader)
            {
                enemies.Add(virus);
                enemies[i].InvadeCell(this.transform);
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
                Anim.SetBool("IsGettingAttacked", true);
            }
        }

        if(progression == 10)
        {
            destroyed = true;
            StartCoroutine(Multiply());
        }
    }

    public override void Remove(List<GameObject> targets){}

    IEnumerator Multiply()
    {
        Anim.SetBool("IsGettingAttacked", false);
        Anim.SetTrigger("Death");
        foreach (Virus enemy in enemies)
        {
            yield return new WaitForSeconds(0.1f); //tempo entre a instanciacao de cada inimigo
            HordeManager.instance.MultiplyVirus(enemy);
            yield return new WaitForSeconds(0.1f); //tempo entre a instanciacao de cada inimigo
            enemy.Evacuate();
        }

        waveToRespawn = HordeManager.instance.waveNumber + 2;
        changeTurretColor(Color.red);
    }

    public void Destroyed()
    {
        if(destroyed == false){
            destroyed = true;
            Anim.SetBool("IsGettingAttacked", false);
            Anim.SetTrigger("NK");
            waveToRespawn = HordeManager.instance.waveNumber + 2;
            foreach (Virus enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
            this.gameObject.GetComponent<Tower>().targets.Clear();
            changeTurretColor(Color.green);
        }
    }

    //Função que troca a cor da torre para indicar a condição de posicionamento em um tile do mapa
    public void changeTurretColor(Color clr)
    {
        //contador
        int i=0;
        //Para cada renderer de objetos filhos da atual turretToBuild
        foreach(Renderer r in this.gameObject.GetComponentsInChildren<Renderer>()){
            //Se a cor parametro for branco, retorne as cores originais do prefab
            if(clr == Color.white)
            {
                r.material.color = StandardColors[i];
                i++;
            }
            //Se a cor não for branco, insira essa cor como a nova cor da torre
            else
            {
                StandardColors.Add(r.material.color);

                r.material.color = clr;      
            }
        }
    }
}