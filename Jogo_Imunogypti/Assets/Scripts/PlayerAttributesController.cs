using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesController : MonoBehaviour
{
	public static PlayerAttributesController instance;
	void Awake(){
		if(instance!=null){
			Debug.LogError("Mais de um PlayerAttributesController");
			return;
		}

		instance = this;
	}

    float Money;
    float Life;

    public Text MoneyText;
    public Text LifeText;

    void Start()
    {
        Money = 800f;
        Life = 100f;
    }
    void Update(){
        MoneyText.text = Money.ToString();
        LifeText.text = Life.ToString();
    }
    // Update is called once per frame
    public float getMoney(){
        return Money;
    }
    public float getLife(){
        return Life;
    }
    public void setMoney(float quant){
        Money = Money + quant;
    
    }
    public void setLife(float quant){
        Life = Life + quant;
       
    }

}
