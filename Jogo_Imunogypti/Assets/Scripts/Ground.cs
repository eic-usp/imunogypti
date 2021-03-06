﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    //Variáveis Renderer e Color
	[SerializeField] private SpriteRenderer rend;
	[SerializeField] private Color defaultColor;
    //Variáveis para posicionar a torre de acordo com a câmera. O parâmetro d fala a posição do plano contendo as torres em relação a câmera
    private Vector3 cameraToPivot;
    private float d = 27.31f;
    private float Xo,Yo,Zo;
    //True caso haja uma torre posicionada neste tile do mapa;
    private bool hasTurret = false;

    void Start()
    {
        //Cor padrão é a cor inicial do prefab
        defaultColor = rend.material.color;

        //Cria o vetor entre o pivot do tile e a main Camera
        cameraToPivot =  transform.position - Camera.main.transform.position;
        //Toma as coordenadas do vetor anterior
     	Xo = cameraToPivot.x;
     	Yo = cameraToPivot.y;
     	Zo = cameraToPivot.z;
    }
    
    void OnMouseOver()
    {
        //Muda a cor do tile
    	rend.material.color = new Color(255,255,255);
        //Se o mouse não estiver pressionado (Torre não está sendo arrastada) e a torre a ser construida pelo buildManager for diferente de null 
        //(Alguma possivelmente torre foi instanciada recentemente)
        if(BuildManager.instance.turretToBuild!=null && Input.GetMouseButton(0)==false && hasTurret==false)
        {
            //Torre não pode mais ser arrastada (impede torre de ser destruida pelo shopping, que seria o caso em que canDrag == true e a torre não nula, mas o mouse
            //não é pressionado)
           	BuildManager.instance.canDrag = false;
            //Chama método para posicionar torre de acordo com a camera
           	BuildManager.instance.SetTurretTransform(Xo,Yo,Zo,d);
             //Retorna a cor padrão da torre
            // BuildManager.instance.changeTurretColor(Color.white);
            //Torre posicionada, coloca torre a ser construida como null para fazer a torre parar de seguir o mouse
            BuildManager.instance.turretToBuild = null;
            //Existe uma torre neste tile
           	hasTurret = true;

        }

        //A ser executado quando uma torre está sendo arrastada sobre esse tile
        if(BuildManager.instance.turretToBuild!=null && Input.GetMouseButton(0)){
            //Se o tile já tiver uma torre (Deixei separado do if de cima pois depois o if abaixo vai ter que ganhar outra cara pra comportar torres que usam mais espaço)
            if(hasTurret){
                    //Se há uma torre aqui, deixar torre vermelha
                    // BuildManager.instance.changeTurretColor(Color.red);
            }
            else{
                //Se não há torres aqui, deixar torre verde
                // BuildManager.instance.changeTurretColor(Color.green);
            }
        }
    }

    void OnMouseExit()
    {
        //Volta a cor original
    	rend.material.color = defaultColor;
        if(BuildManager.instance.turretToBuild!=null){
                //Volta a cor original da torre
                // BuildManager.instance.changeTurretColor(Color.white);
        }
    }

    //Nenhuma utilidade em particular
    void OnMouseDown()
    {
        if(BuildManager.instance.turretToBuild==null)
        {
           return;
        }
    }
}
