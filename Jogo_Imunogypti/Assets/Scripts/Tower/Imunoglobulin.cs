using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Imunoglobulin : MonoBehaviour
{
	private SpawnPoint spawnPoint;
	private Transform target; //Dita a direção do movimento da bala
	public int wavePointIndex=11; //É adicionada de 1 a cada target alcançado

	private Vector3 actualDirection; //Direção na qual o virus está se movendo
    private Vector3 previousDirection; //direção anterior do virus

    private float speed = 5f;
    [SerializeField] private Transform partToRotate;



    // Start is called before the first frame update

    void Awake(){
    	spawnPoint = HordeManager.instance.GetSpawnPoint(UnityEngine.Random.Range(0,HordeManager.instance.getTotalSpawnPoints()));
    }
    void Start()
    {
    	float dL=1000;
    	float previousdL=1000;
    	for(int i=0; i<spawnPoint.points.Length;i++){
    		dL = Vector3.Distance(transform.position, spawnPoint.points[i].transform.position);
    		if(dL<previousdL){
    			wavePointIndex = i;
    			previousdL = dL;
    			
    		}
    	}
    	Vector3 firstDirect = -transform.position + spawnPoint.points[wavePointIndex].transform.position;
    	if(firstDirect.x>0 || firstDirect.y>0)
    			wavePointIndex--;
        target = spawnPoint.points[wavePointIndex];
        actualDirection = -this.transform.position + target.transform.position;

        if(target==null)
        		Debug.Log("Error: Target null");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,target.position) <= 0.25f)
        {
        	GetNextWayPoint();
            actualDirection = -this.transform.position + target.position;
        }

        Rotativ(actualDirection);
        this.transform.Translate(actualDirection.normalized * speed * Time.deltaTime,Space.World);


    }

    void GetNextWayPoint()
    {
        wavePointIndex--;

    	//se ainda tem um proximo ponto para ir, escolhe esse ponto como novo target
    	if(wavePointIndex >= 0)
        {
    			target = spawnPoint.points[wavePointIndex];
    	}
    	//se não o inimigo chegou na base, destroi ele
    	else{
    		Destroy(this.gameObject);
    	}
    }

    private void Rotativ(Vector3 actual)
    {
        partToRotate.transform.rotation = Quaternion.LookRotation(actual, -target.transform.forward);
    }
}
