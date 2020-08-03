using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifocitoMenu : MonoBehaviour
{
    [SerializeField] private Text costText;
    [SerializeField] private Tower tower;
    void Start()
    {
        //ssaaaa
    }

    // Update is called once per frame
    void Update()
    {
        if(costText!=null)
        	costText.text = tower.cost.ToString();
    }
}
