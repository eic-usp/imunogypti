using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;

	void Awake(){
		if(instance!=null){
			Debug.LogError("Mais de um BuildManager");
			return;
		}

		instance = this;
	}

	public GameObject Neutrofilo;
    private GameObject turretToBuild;



    
    public float getPrice(string Tower){

    	if(Tower.Equals("Neutrofilo")){
    		return Neutrofilo.GetComponent<Neutrofilo>().getPrice();
    	}
    	else{
    		return 1;
    	}

    }
    public void SetTurretToBuild(GameObject turret){
        turretToBuild = turret;
    }
    public GameObject GetTurretToBuild(){
    	return turretToBuild;
    }
}
