using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    //Variáveis Renderer e Color
	public SpriteRenderer rend;
	Color defaultColor;

    //Variáveis para serem instancias das classes estáticas do construtor e do shopping.
    BuildManager buildManager;
    Shopping shopping;

    //Variáveis para posicionar a torre de acordo com a câmera. O parâmetro d fala a posição do plano contendo as torres em relação a câmera
    Vector3 cameraToPivot;
    float d = 27.31f;
    float Xo,Yo,Zo;

    //True caso haja uma torre posicionada neste tile do mapa;
    bool hasTurret = false;

    void Start()
    {
        //Cor padrão é a cor inicial do prefab
        defaultColor = rend.material.color;

        //Instancia buildManager e shopping
        buildManager = BuildManager.instance;
        shopping = Shopping.instance;

        //Cria o vetor entre o pivot do tile e a main Camera
        cameraToPivot =  transform.position - Camera.main.transform.position;
        //Toma as coordenadas do vetor anterior
     	Xo = cameraToPivot.x;
     	Yo = cameraToPivot.y;
     	Zo = cameraToPivot.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver(){
        //Muda a cor do tile
    	rend.material.color = new Color(255,255,255);
        //Se o mouse não estiver pressionado (Torre não está sendo arrastada) e a torre a ser construida pelo buildManager for diferente de null 
        //(Alguma possivelmente torre foi instanciada recentemente)
        if(buildManager.GetTurretToBuild()!=null && Input.GetMouseButton(0)==false){
            //Chama método para posicionar torre de acordo com a camera
           	shopping.SetTurretTransform(Xo,Yo,Zo,d);
            //Torre posicionada, coloca torre a ser construida como null para fazer a torre parar de seguir o mouse
            buildManager.SetTurretToBuild(null);
            //Existe uma torre neste tile
           	hasTurret = true;
            //Torre não pode mais ser arrastada (impede torre de ser destruida pelo shopping, que seria o caso em que canDrag == true e a torre não nula, mas o mouse
            //não é pressionado)
           	shopping.canDrag = false;
        }
    }
    void OnMouseExit(){
        //Volta a cor original
    	rend.material.color = defaultColor;
    }

    //Nenhuma utilidade em particular
    void OnMouseDown(){
        if(buildManager.GetTurretToBuild()==null){
           return;
        }
    }
}
