using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAttributesManager : MonoBehaviour
{
	public static AttackAttributesManager instance;
	private float buffAS_neutrofilo=0;
	private float buffD_neutrofilo=0;
	private float buffAS_macrofago=0;
	private float buffD_macrofago=0;
	private float buffD_linfocito=0;

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um AttackAttributesManager");
            return;
        }

        instance = this;
    }

    void Start()
    {
        
    }


    void Update()
    {
        


    }

    public void buffLinfocito(float amountD){
    	GameObject[] linfocitos  = GameObject.FindGameObjectsWithTag("Linfocito");
    	buffD_linfocito += amountD;

    	foreach(GameObject linfocito in linfocitos){
        	IncreaseContinuousDamage effect = linfocito.GetComponent<IncreaseContinuousDamage>();
        	effect.Buff(amountD);
        }
    }

    public void buffNeutrofilo(float amountD, float amountAS){
    	GameObject[] neutrofilos  = GameObject.FindGameObjectsWithTag("Neutrofilo");
    	buffD_neutrofilo += amountD;
    	buffAS_neutrofilo += amountAS;

		foreach(GameObject neutrofilo in neutrofilos){
        	DiscretDamage effect = neutrofilo.GetComponent<DiscretDamage>();
       		effect.Buff(amountD,amountAS);
        }
    }

    public void buffMacrofago(float amountD, float amountAS){
    	GameObject[] macrofagos  = GameObject.FindGameObjectsWithTag("Macrofago");
    	buffD_macrofago +=amountD;
    	buffAS_macrofago +=amountAS;

    	foreach(GameObject macrofago in macrofagos){
       		DiscretDamage effect = macrofago.GetComponent<DiscretDamage>();
       		effect.Buff(amountD,amountAS);
        }

    }

    public float getNeutrofiloBonusD(){
    	return buffD_neutrofilo;
    }
    public float getNeutrofiloBonusAS(){
    	return buffAS_neutrofilo;
    }
    public float getMacrofagoBonusD(){
    	return buffD_macrofago;
    }
    public float getMacrofagoBonusAS(){
    	return buffAS_macrofago;
    }
    public float getLinfocitoBonusD(){
    	return buffD_linfocito;
    }



}
