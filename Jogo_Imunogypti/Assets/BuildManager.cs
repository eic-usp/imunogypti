using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	//Classe estática
	public static BuildManager instance;

	void Awake(){
		if(instance!=null){
			Debug.LogError("Mais de um BuildManager");
			return;
		}

		instance = this;
	}
	//GameObjects com diferentes torres e um objeto turretToBuild que é a torre a ser construida
	public GameObject Neutrofilo;
    private GameObject turretToBuild;



    //Devolve preço da torre de acordo com seu nome
    public float getPrice(string Tower){

    	//Se a torre for o neutrofilo, devolve o preço dele
    	if(Tower.Equals("Neutrofilo")){
    		return Neutrofilo.GetComponent<Neutrofilo>().getPrice();
    	}
    	else{
    		//Aqui devem ir os ifs para outras torres
    		return 1;
    	}

    }
    //Insere torre a ser construida
    public void SetTurretToBuild(GameObject turret){
        turretToBuild = turret;
    }
    //Devolve torre a ser construida
    public GameObject GetTurretToBuild(){
    	return turretToBuild;
    }
}
