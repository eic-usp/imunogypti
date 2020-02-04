using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEnemy : MonoBehaviour, IRotate
{
   	[SerializeField] private Transform partToRotate;

    public void LookAt(Transform target, Transform tower)
    {
        //A direção entre a torre e o inimigo
    	Vector3 dir = target.position - transform.position;
    	//Quaternion com rotação para fazer a torre olhar pro inimigo
    	Quaternion lookRotation = Quaternion.LookRotation(dir);
    	Vector3 rotation = lookRotation.eulerAngles;

        partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y,0f);
    }
}
