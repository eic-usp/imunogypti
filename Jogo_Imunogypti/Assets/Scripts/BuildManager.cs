using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sistema da compra e evolucao de torres do jogo 
public class BuildManager : MonoBehaviour
{
    //GameObjects com diferentes torres e um objeto turretToBuild que é a torre a ser construida
	[SerializeField] private Tower Neutrofilo;
    private int neutrofiloCost = 100;
    public Tower turretToBuild; //Torre a ser instanciada


    public bool canDrag = false; //Booleano que indica quando a torre pode ser arrastada pelo mapa
	public static BuildManager instance; //Classe estática

	void Awake()
    {
		if(instance!=null)
        {
			Debug.LogError("Mais de um BuildManager");
			return;
		}

		instance = this;
	}

    void Update()
    {
        //Se o mouse estiver pressionado e o objeto turret não nulo puder ser arrastado
        if(Input.GetMouseButton(0) && canDrag==true && turretToBuild!=null)
            turretToBuild.transform.position = GetMouseWorldPos(); //Seta posição da instancia da torre como a posição do mouse

        //Caso em que a torre é solta em uma área não alocavel do mapa
        else
        { 
            if(canDrag==true && turretToBuild!=null)
            {
                Debug.Log("Torre destruida");
                canDrag = false;
                Shopping.instance.EarnGold(neutrofiloCost); //Recupera o dinheiro por não ter posicionado a torre no mapa
                Destroy(turretToBuild);
                return;
            }
        }
    }

    //funcao seta a torre a ser instalada no mapa
    public void Install(Tower tower)
    {
        //verifica se o jogador tem dinheiro o suficiente para fazer a compra
        if(!Shopping.instance.ShellOut(neutrofiloCost))
            return;

        //Instancia a torre em coordenadas quaisquer e habilita o canDrag
        turretToBuild = Instantiate(tower,new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,0)));
        canDrag = true;
    }

    //Método que dá a posição do mouse com um z fixo para posicionar a torre junto ao mouse enquanto o player a arrastar
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 15;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    //Posiciona torre com o referencial na camera em um plano paralelo à camera a uma distancia d. As coordenadadas Xo, Yo, Zo são os vetores entre a camera e o pivot do tile do mapa
    public void SetTurretTransform(float Xo, float Yo, float Zo, float d)
    {
        if(turretToBuild!=null)
        {
            turretToBuild.transform.parent = Camera.main.transform;
            turretToBuild.transform.localPosition = new Vector3((d/Zo)*Xo,(d/Zo)*Yo,d);
            //Rotação de acordo com a rotação escolhida para o mapa
            turretToBuild.transform.rotation = Quaternion.Euler(-30,0,0);
        }
    }
}
