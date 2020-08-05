using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    //Variáveis Renderer e Color
	[SerializeField] private SpriteRenderer rend;
	[SerializeField] private Color defaultColor;

    void Start()
    {
        //Cor padrão é a cor inicial do prefab
        defaultColor = rend.material.color;
    }

    void OnMouseOver()
    {
        //Muda a cor do tile
    	rend.material.color = new Color(255,255,255);
        //Se o mouse não estiver pressionado (Torre não está sendo arrastada) e a torre a ser construida pelo buildManager for diferente de null 
        //(Alguma possivelmente torre foi instanciada recentemente)
        // if(BuildManager.instance.turretToBuild!=null && Input.GetMouseButton(0)==false && hasTurret==false)
        // {
        //     //Torre não pode mais ser arrastada (impede torre de ser destruida pelo shopping, que seria o caso em que canDrag == true e a torre não nula, mas o mouse
        //     //não é pressionado)
        //    	BuildManager.instance.canDrag = false;
        //     //Chama método para posicionar torre de acordo com a camera
        //    	BuildManager.instance.SetTurretTransform(Xo,Yo,Zo,d);
        //      //Retorna a cor padrão da torre
        //     BuildManager.instance.changeTurretColor(Color.white);
        //     //Torre posicionada, coloca torre a ser construida como null para fazer a torre parar de seguir o mouse
        //     BuildManager.instance.turretToBuild = null;
        //     //Existe uma torre neste tile
        //    	hasTurret = true;

        // }

        //A ser executado quando uma torre está sendo arrastada sobre esse tile
        if(BuildManager.instance.turretToBuild!=null && Input.GetMouseButton(0)){
            //pinta a torre de vermelho porque ela não pdoe ser instalada no caminho
            BuildManager.instance.changeTurretColor(Color.red);

        }
    }

    void OnMouseExit()
    {
        //Volta a cor original
    	rend.material.color = defaultColor;
        if(BuildManager.instance.turretToBuild!=null){
                //Volta a cor original da torre
                BuildManager.instance.changeTurretColor(Color.white);
        }
    }

    //Desseleciona torre
    void OnMouseDown()
    {
        TowerSelection.instance.Hide();
    }
}
