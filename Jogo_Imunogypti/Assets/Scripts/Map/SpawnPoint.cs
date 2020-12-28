using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //Pontos que darão a trajetoria do virus, variável estática para ser acessada remotamente
    public Transform[] points;
	[SerializeField] private Base b;

    void Awake(){
    	//Seta pontos como os objetos filhos do objeto waypoints
    	points = new Transform[transform.childCount];
    	for(int i = 0; i < points.Length; i++)
    	{
    		points[i] = transform.GetChild(i);
    	}

		//b.BaseLocate(points[points.Length-1].position);
    }
}
