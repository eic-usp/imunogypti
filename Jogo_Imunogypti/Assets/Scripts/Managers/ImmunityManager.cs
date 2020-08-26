using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImmunityManager : MonoBehaviour
{
    [SerializeField] private float immunity;
    public static ImmunityManager instance;
    [SerializeField] private Text immunityText;

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um ImmunityManager");
            return;
        }

        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        immunityText.text = ((int)immunity).ToString();
    }

    public void Increase(float amount){
    	immunity +=amount;
    }

    public void Decrease(float amount){
    	immunity-=amount;
    }

    public float getImmunity(){
        return immunity;
    }
}
