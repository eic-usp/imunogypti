using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    //Classe estática que faz a compra das torres
    public static Shopping instance;

    void Awake(){
        if(instance!=null){
            Debug.LogError("Mais de um Shopping");
            return;
        }

        instance = this;
    }

    //Instancia da classe construtora de torres
    BuildManager buildManager;
    //Torre a ser instanciada
    public GameObject turret;
    //Booleano que indica quando a torre pode ser arrastada pelo mapa
    public bool canDrag = false;
    float price;

    //Atributos do jogador
    PlayerAttributesController playattb;

    void Start()
    {
        buildManager = BuildManager.instance;
        playattb = PlayerAttributesController.instance;
    }


    void Update()
    {
        //Se o mouse estiver pressionado e o objeto turret não nulo puder ser arrastado
        if(Input.GetMouseButton(0) && canDrag==true && turret!=null){
            //Verifica se a torre a ser  construida no buildManager é nula para fins de controle
            if(buildManager.GetTurretToBuild()!=null){
                //Seta posição da instancia da torre como a posição do mouse
                turret.transform.position = GetMouseWorldPos();
            }
        }
        else{
            //Caso em que a torre é solta em uma área não alocavel do mapa
            if(canDrag==true && turret!=null){
                Debug.Log("Torre destruida");
                canDrag = false;
                Destroy(turret);
                //Recupera o dinheiro por não ter posicionado a torre no mapa
                playattb.setMoney(price);
                return;
            }
        }
    }
    //Instancia um neutrofilo como turret
    public void BuyNeutrofilo(){
        //Pega o preço do neutrofilo
         price = buildManager.getPrice("Neutrofilo");
        //Verifica se o preço é menor ou igual ao o dinheiro atual
        if(playattb.getMoney()>=price){
            //Escolhe Neutrofilo como a torre a ser construida pelo buildManager
            buildManager.SetTurretToBuild(buildManager.Neutrofilo);
        }
        //Instancia a torre em coordenadas quaisquer e habilita o canDrag
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild,new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,0)));
        canDrag = true;

        playattb.setMoney(-price);

    }
    //Método que dá a posição do mouse com um z fixo para posicionar a torre junto ao mouse enquanto o player a arrastar
    private Vector3 GetMouseWorldPos(){
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 15;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    //Posiciona torre com o referencial na camera em um plano paralelo à camera a uma distancia d. As coordenadadas Xo, Yo, Zo são os vetores entre a camera e o pivot do tile do mapa
    public void SetTurretTransform(float Xo, float Yo, float Zo, float d){
        if(turret!=null){
        turret.transform.parent = Camera.main.transform;
        turret.transform.localPosition = new Vector3((d/Zo)*Xo,(d/Zo)*Yo,d);
        //Rotação de acordo com a rotação escolhida para o mapa
        turret.transform.rotation = Quaternion.Euler(-30,0,0);
        }
    }
}
