using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEnemy : MonoBehaviour, IRotate
{
   	[SerializeField] private Transform partToRotate;
    // Rigidbody rb;


    void Start(){
        partToRotate.transform.rotation = Quaternion.Euler(0, 180f, 0);
    }

    public void LookAt(Transform target, Transform tower)
    {
        //A direção entre a torre e o inimigo
    	Vector3 dir = target.transform.position - partToRotate.transform.position;
    	//Quaternion com rotação para fazer a torre olhar pro inimigo
    	// Quaternion lookRotation = Quaternion.LookRotation(dir,tower.forward);
    	// Vector3 rotation = lookRotation.eulerAngles;

        // partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y, 0f);



        // partToRotate.rotation = Quaternion.LookRotation(dir, tower.forward);



    	dir = new Vector3 (dir.x, dir.y, transform.position.z);
        // transform.LookAt(dir);
        // // transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,transform.rotation.z);
        // var a = transform.eulerAngles.y;
        // transform.eulerAngles = new Vector3 (-90f, transform.eulerAngles.y, 0f);  
        // Debug.Log(transform.eulerAngles.y + ", " + a);



        // dir.z = 0;
        // dir.x = -90;
        // dir.y = 0;
        // Quaternion newRotation = Quaternion.LookRotation(dir);
        // rb.MoveRotation(newRotation);
        
        
         
        // actualDirection = -this.transform.position + target.transform.position;

        partToRotate.transform.rotation = Quaternion.LookRotation(dir, -target.transform.forward);
    }
}
