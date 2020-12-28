using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCell : BaseNoAttack
{

    Vector3 direction;
    public bool hasReached = false;
    public bool goAway = false;
    Vector3 startPosition;
    public Vector3 target;
    public MultipliesViruses cell;

    public void Start(){
        startPosition = this.transform.position;

    }
    public override void Apply(List<GameObject> targets)
    {
        if(targets.Count <= 0) {
            return;
        }
        cell = targets[0].GetComponent<MultipliesViruses>();
        direction  = targets[0].transform.position - this.transform.position;
        target = targets[0].transform.position;
        //Destroy(this.gameObject);

    }

    public void Update(){
        if(Vector3.Distance(this.transform.position,target)<0.25 && goAway==false)
            hasReached = true;

        if(!hasReached){
            this.transform.Translate(direction.normalized * 10f* Time.deltaTime,Space.World);
        }
        else if(hasReached && !goAway){
            cell.Destroyed();
            direction = new Vector3(10,10,10);
            goAway = true;
        }
        if(goAway){
             Vector3 direction2 = -this.transform.position + startPosition; 
            this.transform.Translate(direction2.normalized * 10f *Time.deltaTime,Space.World);
        }
        if(goAway && Vector3.Distance(this.transform.position,startPosition)<0.25){
            Destroy(this.gameObject);
        }
    }

    public override void Remove(List<GameObject> targets){}
}
