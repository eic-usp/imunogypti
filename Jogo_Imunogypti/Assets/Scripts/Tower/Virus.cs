using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Classe que representa os inimigos que devem ser derrotados
public class Virus : MonoBehaviour
{
    public float hp; //vida do inimigo
    [SerializeField] private float hpI; //vida inicial do inimigo
    public int damage; //dano que o inimigo da ao jogador quando chega ao fim do caminho
    [SerializeField] private float speed; //velocidade com que o inimigo caminha pelo mapa
    [SerializeField] private int goldValue; //dinheiro que o inimigo da ao jogador quando eh destruido
    //[SerializeField] private Color color; // cor/sprite do inimigo
    public SpawnPoint spawnPoint; //ponto de onde o inimigo saiu, guarda a trajetoria do virus até a base
    [SerializeField] private Transform target; //Dita a direção do movimento do virus
    public int wavePointIndex=0; //É adicionada de 1 a cada target alcançado
    public bool stop = false;
    public bool invader = false;
    Animator Anim; //Animator do virus
    AudioSource Audio;

    private Vector3 actualDirection; //Direção na qual o virus está se movendo
    private Vector3 previousDirection; //direção anterior do virus

    public float spawnTime= 0.5f;

    [SerializeField] private Transform partToRotate;
    [SerializeField] private Transform lifeBar;

    void Start()
    {
        //Alvo inicial é o primeiro waypoint 
        target = spawnPoint.points[wavePointIndex];
        actualDirection = -this.transform.position + target.transform.position;
        stop = false;
        //Se o waypoint for nulo
        if(target==null)
        		Debug.Log("Error: Target null");

        hp = hpI;
        Anim = gameObject.GetComponent<Animator>(); //Pega animator vinculado ao GameObject do virus
        Anim.SetFloat("Offset", Random.Range(0.0f, 1.0f));
       
    }

    void Update()
    {

        if(stop)
            return;
        //Se a distancia entre o virus e o target é muito pequena, chame o método pra mudar de target
        if(Vector3.Distance(this.transform.position,target.position) <= 0.4f)
        {
        	GetNextWayPoint();
            actualDirection = -this.transform.position + target.position;
        }

        Rotativ(actualDirection); 
        //actualDirection = -this.transform.position + target.transform.position;
        // a Direção é o vetor que liga o virus ao target
        //this.transform.LookAt(Camera.main.transform.position);
        //Move o virus na direção com uma velocidade 'speed' em relação ao World
        this.transform.Translate(actualDirection.normalized * speed * Time.deltaTime,Space.World);
        Audio = this.GetComponent<AudioSource>();
    }

    //funcao que pega o ponto para onde o virus deve ir
    void GetNextWayPoint()
    {
        wavePointIndex++;

    	//se ainda tem um proximo ponto para ir, escolhe esse ponto como novo target
    	if(wavePointIndex < spawnPoint.points.Length)
        {
    			target = spawnPoint.points[wavePointIndex];
    	}
    	//se não o inimigo chegou na base, destroi ele
    	else{
            LifeManager.instance.TakeDamage(damage);
    		Destroy(this.gameObject);
    	}
    }

    //funcao que da dano na vida do inimigo
    public void DealDamage(float damage)
    {
        if(!lifeBar.gameObject.active){
            lifeBar.gameObject.SetActive(true);
        }
        hp -= damage;
        //mata o  inimigo quando a vida chega a 0
        if(hp <= 0) {
            Vector3 lScale = lifeBar.transform.GetChild(0).localScale;
            lifeBar.transform.GetChild(0).localScale = new Vector3(0, lScale.y, lScale.z);
            Killed();
        }
        //se ainda estiver vivo, atualiza a barra
        else {
            Vector3 lScale = lifeBar.transform.GetChild(0).localScale;
            float deltaL = (damage/hpI);
            lifeBar.transform.GetChild(0).localScale = new Vector3(lScale.x+deltaL, lScale.y, lScale.z);
        }
    }

    //funcao de quando o inimigo eh morto
    public void Killed()
    {
        Shopping.instance.EarnGold(goldValue);
        stop = true;
        Anim.SetTrigger("Death");
        FindObjectOfType<AudioManager>().Play("oof");
        Destroy(this.gameObject, 1.5f);
    }

    private void OnDestroy() 
    {
        HordeManager.instance.activeViruses--;    
    }

    private void Rotativ(Vector3 actual)
    {
        partToRotate.transform.rotation = Quaternion.LookRotation(actual, -target.transform.forward);
    }

    public float getSpawnTime()
    {
        return spawnTime;
    }

    public void InvadeCell(Transform cell)
    {
        actualDirection = -this.transform.position + cell.position;
        invader = true;
        Anim.SetBool("Attack", true);
        Audio.Pause();        
    }
    
    public void Evacuate()
    {
        actualDirection = -this.transform.position + target.position;
        stop = false;
        Anim.SetBool("Attack", false);
        Audio.Play();
    }
}
