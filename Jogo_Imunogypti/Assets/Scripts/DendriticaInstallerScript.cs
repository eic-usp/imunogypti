using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DendriticaInstallerScript : MonoBehaviour
{
    [SerializeField] private Tower celulaDendritica;
    private Ground myGround;

    public void InstallDendritica()
    {
        myGround = GetComponent<Ground>();
        myGround.tower = Instantiate(celulaDendritica,transform.position,Quaternion.Euler(-90f,0f,0f));
    }
}
