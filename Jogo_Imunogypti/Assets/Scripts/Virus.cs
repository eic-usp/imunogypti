using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Classe que representa os inimigos que devem ser derrotados
public class Virus : MonoBehaviour
{
    public float hp; //vida do inimigo
    private float hpI;
    public int damage; //dano que o inimigo da ao jogador quando chega ao fim do caminho
    [SerializeField] private float speed; //velocidade com que o inimigo caminha pelo mapa
    [SerializeField] private int goldValue; //dinheiro que o inimigo da ao jogador quando eh destruido
    //[SerializeField] private Color color; // cor/sprite do inimigo
    private Transform target; //Dita a direção do movimento do virus
    private int wavePointIndex=0; //É adicionada de 1 a cada target alcançado

    private Vector3 actualDirection; //Direção na qual o virus está se movendo
    private Vector3 previousDirection; //direção anterior do virus

    [SerializeField] private Transform partToRotate;
    [SerializeField] private Transform lifeBar;

    void Start()
    {
        //Alvo inicial é o primeiro waypoint 
        target = Waypoints.points[0];
        actualDirection = -this.transform.position + target.transform.position;
        //Se o waypoint for nulo
        if(target==null)
        		Debug.Log("Error: Target null");

        hpI = hp;
       
    }

    void Update()
    {
        //Se a distancia entre o virus e o target é muito pequena, chame o método pra mudar de target
        if(Vector3.Distance(transform.position,target.position)<=0.25f){
        	GetNextWayPoint();
            previousDirection = actualDirection;
            actualDirection = -this.transform.position + target.transform.position;
        }
        Rotativ(previousDirection,actualDirection); 
        //actualDirection = -this.transform.position + target.transform.position;
    	// a Direção é o vetor que liga o virus ao target
        //this.transform.LookAt(Camera.main.transform.position);
        //Move o virus na direção com uma velocidade 'speed' em relação ao World
        this.transform.Translate(actualDirection.normalized * speed * Time.deltaTime,Space.World);

        //TurnToNormal();
    }

    //funcao que pega o ponto para onde o virus deve ir
    void GetNextWayPoint(){
    	//Se o virus está prestes a sair do ultimo target, destrua
    	if(wavePointIndex>=Waypoints.points.Length-1){
    		Destroy(this.gameObject);
    	}
    	//Senão, acrescente 1 ao index e mude de target
    	else{
    			wavePointIndex++;
    			target = Waypoints.points[wavePointIndex];
    	}
    }

    //funcao que da dano na vida do inimigo
    public void DealDamage(float damage)
    {
        if(!lifeBar.gameObject.active){
            lifeBar.gameObject.SetActive(true);
        }
        hp -= damage;
        Vector3 lScale = lifeBar.transform.GetChild(0).localScale;
        float deltaL = (damage/hpI);
        lifeBar.transform.GetChild(0).localScale = new Vector3(lScale.x - deltaL,lScale.y,lScale.z);
        //mata o  inimigo quando a (nao) vida chega a 0
        if(hp <= 0)
            Killed();
    }

    //funcao de quando o inimigo eh morto
    public void Killed()
    {
        Shopping.instance.EarnGold(goldValue);
        Destroy(this.gameObject);
    }

    private void OnDestroy() 
    {
        SpawnPoint.instance.activeViruses--;    
    }


    private void Rotativ(Vector3 prev, Vector3 actual){

        partToRotate.transform.rotation = Quaternion.LookRotation(actual,-target.transform.forward);
        

    }
    
}
