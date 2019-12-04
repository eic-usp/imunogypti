using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
	public SpriteRenderer rend;
	Color defaultColor;
	//private GameObject turret;
    BuildManager buildManager;
    bool hasTurret = false;
    Shopping shopping;

    //VARIAVEIS PARA POSICIONAR A TORRE DE ACORDO COM A CAMERA
    Vector3 cameraToPivot;
    float d = 27.31f;
    float Xo,Yo,Zo;

    void Start()
    {
        defaultColor = rend.material.color;
        buildManager = BuildManager.instance;
        shopping = Shopping.instance;

        cameraToPivot =  transform.position - Camera.main.transform.position;
     	Xo = cameraToPivot.x;
     	Yo = cameraToPivot.y;
     	Zo = cameraToPivot.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver(){
    	rend.material.color = new Color(255,255,255);
        if(buildManager.GetTurretToBuild()!=null && Input.GetMouseButton(0)==false){
           	shopping.SetTurretTransform(Xo,Yo,Zo,d);
            buildManager.SetTurretToBuild(null);
           	hasTurret = true;
           	shopping.canDrag = false;
        }
    }
    void OnMouseExit(){
    	rend.material.color = defaultColor;
    }
    void OnMouseDown(){
        if(buildManager.GetTurretToBuild()==null){
           return;
        }
    }
}
