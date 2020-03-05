using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa os inimigos que devem ser derrotados
public class Virus : MonoBehaviour
{
    public float hp; //vida do inimigo
    public int damage; //dano que o inimigo da ao jogador quando chega ao fim do caminho
    [SerializeField] private float speed; //velocidade com que o inimigo caminha pelo mapa
    [SerializeField] private int goldValue; //dinheiro que o inimigo da ao jogador quando eh destruido
    //[SerializeField] private Color color; // cor/sprite do inimigo
    private Transform target; //Dita a direção do movimento do virus
    private int wavePointIndex=0; //É adicionada de 1 a cada target alcançado

    private Vector3 actualDirection; //Direção na qual o virus está se movendo
    private Vector3 previousDirection; //direção anterior do virus

    public GameObject a ,b,c; //Teste para calcular normal
    private Vector3 normal;
    void Start()
    {
        //Alvo inicial é o primeiro waypoint 
        target = Waypoints.points[0];
        actualDirection = -this.transform.position + target.transform.position;
        //Se o waypoint for nulo
        if(target==null)
        		Debug.Log("Error: Target null");

        Vector3 side1 = b.transform.position - a.transform.position;
        Vector3 side2 = c.transform.position - a.transform.position;
         normal = Vector3.Cross(side1,side2);
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
        hp -= damage;
        
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
       //transform.LookAt(new Vector3(target.position.x,target.position.y,0));
        Quaternion XLookRotation = Quaternion.LookRotation(actual, Vector3.right) * Quaternion.Euler(new Vector3(0,-90f,0));
        Quaternion YLookRotation = Quaternion.LookRotation(normal,Vector3.up) * Quaternion.Euler(new Vector3(getSignal(prev)*90f,0f,0));

        transform.rotation = XLookRotation * YLookRotation;
        //transform.rotation = XLookRotation;
        //transform.rotation = Quaternion.Euler( XLookRotation.eulerAngles.x,XLookRotation.eulerAngles.y,XLookRotation.eulerAngles.z);
        

    }
    private int getSignal(Vector3 prev){
        Debug.Log(prev);
        int signal = 0;
       if(prev.y<0){
           signal = -1;
       }
       if(prev.y>0 || prev.x<0){
           signal = 1;
       }
       return signal;
    }

    
}
