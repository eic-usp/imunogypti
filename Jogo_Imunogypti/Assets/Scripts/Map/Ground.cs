using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    //Variáveis Renderer e Color
	[SerializeField] private SpriteRenderer rend;
	[SerializeField] private Color originalColor;
	[SerializeField] private Color defaultColor;
    //Variáveis para posicionar a torre de acordo com a câmera. O parâmetro d fala a posição do plano contendo as torres em relação a câmera
    private Vector3 cameraToPivot;
    private float d = 27.31f;
    private float Xo,Yo,Zo;
    //True caso haja uma torre posicionada neste tile do mapa;
    public Tower tower;
    [SerializeField] private bool activeLinfocitos = false;
    [SerializeField] private bool buffMacrofago = false;

    private float buffDamage = 0;
    private float buffAtackSpeed = 0;

    void Start()
    {
        //Cor padrão é a cor inicial do prefab
        rend.material.color = defaultColor;
        
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
    	rend.material.color = Color.white;
        //Se o mouse não estiver pressionado (Torre não está sendo arrastada) e a torre a ser construida pelo buildManager for diferente de null 
        //(Alguma possivelmente torre foi instanciada recentemente)
        if(BuildManager.instance.turretToBuild!=null && Input.GetMouseButton(0)==false)
        {
            if(BuildManager.instance.turretToBuild.name == "NaturalKiller")
            {
                if(tower != null && tower.name == "Cell")
                {
                    //Torre não pode mais ser arrastada (impede torre de ser destruida pelo shopping, que seria o caso em que canDrag == true e a torre não nula, mas o mouse
                    //não é pressionado)
                    BuildManager.instance.canDrag = false;
                    //Chama método para posicionar torre
                    BuildManager.instance.SetNaturalKiller(tower);
                    // volta pra cor normal
                    BuildManager.instance.changeTurretColor(Color.white);
                    //Torre posicionada, coloca torre a ser construida como null para fazer a torre parar de seguir o mouse
                    BuildManager.instance.turretToBuild = null;
                }
            }
            else if(tower == null)
            {
                //Torre não pode mais ser arrastada (impede torre de ser destruida pelo shopping, que seria o caso em que canDrag == true e a torre não nula, mas o mouse
                //não é pressionado)
                BuildManager.instance.canDrag = false;
                //Chama método para posicionar torre de acordo com a camera
                BuildManager.instance.SetTurretTransform(this.transform);
                //Retorna a cor padrão da torre
                BuildManager.instance.changeTurretColor(Color.white);
                //Existe uma torre neste tile
                tower = BuildManager.instance.turretToBuild;
                //Torre posicionada, coloca torre a ser construida como null para fazer a torre parar de seguir o mouse
                BuildManager.instance.turretToBuild = null;

                //Verifica se precisa ativar algo na torre
                if(activeLinfocitos)
                    tower.Activate();    
                if(buffMacrofago)
                {
                    DiscretDamage dD = tower.GetComponent<DiscretDamage>();
                    if(dD != null)
                        dD.Buff(buffAtackSpeed, buffDamage);
                }
            }
        }

        //A ser executado quando uma torre está sendo arrastada sobre esse tile
        if(BuildManager.instance.turretToBuild!=null && Input.GetMouseButton(0))
        {
            if(BuildManager.instance.turretToBuild.name == "NaturalKiller")
            {
                if(tower != null && tower.name == "Cell")
                    BuildManager.instance.changeTurretColor(Color.green);
                else   
                    BuildManager.instance.changeTurretColor(Color.red);
            }
            else
            {
                //Se o tile já tiver uma torre (Deixei separado do if de cima pois depois o if abaixo vai ter que ganhar outra cara pra comportar torres que usam mais espaço)
                if(tower != null)
                {
                    //Se há uma torre aqui, deixar torre vermelha
                    BuildManager.instance.changeTurretColor(Color.red);
                }
                else if(BuildManager.instance.turretToBuild.active == false && activeLinfocitos == false)
                {
                    BuildManager.instance.changeTurretColor(Color.yellow);
                }
                else
                {
                    //Se não há torres aqui, deixar torre verde
                    BuildManager.instance.changeTurretColor(Color.green);
                }
            }
        }
    }

    void OnMouseExit()
    {
        //Volta a cor original
    	rend.material.color = defaultColor;
        if(BuildManager.instance.turretToBuild!=null)
        {
            //Volta a cor original da torre
            BuildManager.instance.changeTurretColor(Color.white);
        }
    }

    //Seleciona torre
    void OnMouseDown()
    {
        if(tower == null || tower.name == "Cell")
            TowerSelection.instance.Hide();
        else
            TowerSelection.instance.Select(this);
    }

    public void Activate()
    {
        activeLinfocitos = true;
        defaultColor = Color.yellow;
        rend.material.color = defaultColor;

        if(tower != null)
            tower.Activate();
    }
    
    public void Deactivate()
    {
        activeLinfocitos = false;
        defaultColor = originalColor;
        rend.material.color = defaultColor;

        if(tower != null && tower.CompareTag("Linfocito"))
            tower.Deactivate();
    }
    
    public void ActivateBuffMacrofago(float buffAS, float buffD)
    {
        float difBuffAtackSpeed = Mathf.Max(buffAtackSpeed, buffAS) - buffAtackSpeed;
        float difBuffDamage = Mathf.Max(buffDamage, buffD) - buffDamage;
        buffAtackSpeed = Mathf.Max(buffAtackSpeed, buffAS);
        buffDamage = Mathf.Max(buffDamage, buffD);

        if(tower != null)
        {
            DiscretDamage dD = tower.GetComponent<DiscretDamage>();
            if(dD != null)
                dD.Buff(difBuffAtackSpeed, difBuffDamage);
        }

        buffMacrofago = true;
        defaultColor = Color.magenta;
        rend.material.color = defaultColor;
    }

    public void DeactivateBuffMacrofago()
    {
        if(tower != null)
        {
            DiscretDamage dD = tower.GetComponent<DiscretDamage>();
            if(dD != null)
                dD.Buff(-buffAtackSpeed, -buffDamage);
        }

        buffAtackSpeed = 0;
        buffDamage = 0;

        buffMacrofago = false;
        if(activeLinfocitos)
            defaultColor = Color.yellow;
        else
            defaultColor = originalColor;
        rend.material.color = defaultColor;
    }

    public void Uninstall()
    {  
        if(tower.name == "Celula Dendritica")
            tower.Reset();
        else
        {
            tower.Uninstall();
            tower = null;
        }
    }
}
