using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex=0;

    void Start()
    {
        target = Waypoints.points[0];
        if(target==null)
        		Debug.Log("Socorro");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = -this.transform.position + target.transform.position;
        this.transform.LookAt(Camera.main.transform.position);
        this.transform.Translate(dir.normalized * 5f * Time.deltaTime,Space.World);

        if(Vector3.Distance(transform.position,target.position)<=0.2f){
        	GetNextWayPoint();
        }


    }
    void GetNextWayPoint(){

    	if(wavePointIndex>=Waypoints.points.Length-1){
    		Destroy(gameObject);
    	}
    	else{
    			wavePointIndex++;
    			target = Waypoints.points[wavePointIndex];
    	}
    }
}
