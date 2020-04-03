using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe que representa o basico de todas as torres
public class Tower : MonoBehaviour
{
    [SerializeField] public bool active = false; //define se a torre esta ativa
    [SerializeField] private float range = 5f; //alcance da torre, ex: do ataque
    public int upgradeCost;
    public int cost;
    public GameObject rangeCircle;
    public bool drawRange = false;

    private int ID;

    public Tower[] towers;

   	private List<GameObject> targets;

    //Interfaces
   	private ITarget myTarget;
   	private IRotate myRotate;
   	private IEffect myEffect;

	void Awake()
    {
        myTarget = GetComponent<ITarget>();
        myRotate = GetComponent<IRotate>();
        myEffect = GetComponent<IEffect>();
        rangeCircle = this.gameObject.transform.GetChild(2).gameObject;
        rangeCircle.transform.localScale *=(range+2);
        ID =GameObject.FindGameObjectsWithTag(this.gameObject.tag).Length;


    }

    void Update()
    {   
        if(active==false)
            return;

		targets = myTarget.UpdateTarget(range);

    	//Rotaciona torre para olhar na direção do inimigo
        if(targets.Count != 0)
            myRotate.LookAt(targets[0].transform, transform);

        myEffect.Apply(targets);

        if(drawRange == true){
            rangeCircle.SetActive(true);
        }
        else{
            rangeCircle.SetActive(false);
        }
    }
    private void OnMouseDown() {
        towers = FindObjectsOfType(typeof(Tower)) as Tower[];
        foreach (Tower t in towers )
        {
            if(!t.Equals(this))
                t.drawRange = false;
        }
        Debug.Log("This is sparta");
        drawRange = !drawRange;
        EvolvePanel.instance.showEvolvePanel(this,drawRange);
    }
    public void Activate()
    {
        active = true;
    }
    
    public void Deactivate()
    {
        active = false;
    }

    public void Uninstall()
    {  
        myEffect.Remove(targets);
        Destroy(this.gameObject);
    }

    public float getRange(){
        return range;
    }
    public int getID()
    {
        return ID;
    }
}
