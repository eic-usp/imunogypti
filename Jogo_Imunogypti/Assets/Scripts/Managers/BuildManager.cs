using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sistema da compra e evolucao de torres do jogo 
public class BuildManager : MonoBehaviour
{
    //GameObjects com diferentes torres e um objeto turretToBuild que é a torre a ser construida
    public Tower turretToBuild; //Torre a ser instanciada
    private List<Color> StandardColors = new List<Color>();//Lista com cores padrão da torre
    public bool canDrag = false; //Booleano que indica quando a torre pode ser arrastada pelo mapa
    [SerializeField] Transform pistaDePouso;
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
                Shopping.instance.EarnGold(turretToBuild.cost); //Recupera o dinheiro por não ter posicionado a torre no mapa
                Destroy(turretToBuild.gameObject);
                return;
            }
        }
    }

    //funcao seta a torre a ser instalada no mapa
    public void Install(Tower tower)
    {
        if(turretToBuild==null)
        {
            //verifica se o jogador tem dinheiro o suficiente para fazer a compra
            if(!Shopping.instance.ShellOut(tower.cost))
               return;

            //Instancia a torre em coordenadas quaisquer e habilita o canDrag
            turretToBuild = Instantiate(tower,new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,0)));
            //Para cada renderer de objeto filho da atual turretToBuild, grave sua cor na lista de cores padrão do prefab
            StandardColors.Clear();
            foreach(Renderer r in turretToBuild.GetComponentsInChildren<Renderer>())
                StandardColors.Add(r.material.color);
            canDrag = true;
        }
    }

    //Método que dá a posição do mouse com um z fixo para posicionar a torre junto ao mouse enquanto o player a arrastar
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 15;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    //Posiciona torre com o referencial na camera em um plano paralelo à camera a uma distancia d. As coordenadadas Xo, Yo, Zo são os vetores entre a camera e o pivot do tile do mapa
    public void SetTurretTransform(Transform t)
    {
        if(turretToBuild!=null)
        {
            turretToBuild.transform.position = t.position;
            //Rotação de acordo com a rotação escolhida para o mapa
            turretToBuild.transform.rotation = Quaternion.Euler(-90f,0f,0f);
        }
    }

    public void SetNaturalKiller(Tower tower){
        if(turretToBuild!=null)
        {
            Vector3 pos = new Vector3(pistaDePouso.position.x,pistaDePouso.position.y,pistaDePouso.position.z - 10f);
            turretToBuild.transform.position = pos;
            Tower nK = turretToBuild.GetComponent<Tower>();
            nK.Activate();
            nK.targets.Add(tower.gameObject);

            //Rotação de acordo com a rotação escolhida para o mapa
            //turretToBuild.transform.rotation = Quaternion.Euler(-90f,0f,0f);
        }
    }

    //Função que troca a cor da torre para indicar a condição de posicionamento em um tile do mapa
    public void changeTurretColor(Color clr){
        //contador
        int i=0;
        //Para cada renderer de objetos filhos da atual turretToBuild
        foreach(Renderer r in turretToBuild.GetComponentsInChildren<Renderer>()){
            //Se a cor parametro for branco, retorne as cores originais do prefab
            if(clr == Color.white){
                r.material.color = StandardColors[i];
                i++;
            }
            //Se a cor não for branco, insira essa cor como a nova cor da torre
            else
               r.material.color = clr;      
        }
    }
}
