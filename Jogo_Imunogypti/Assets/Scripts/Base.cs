using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int hpIni;
    [SerializeField] private Text hpText;
    public GameObject EndScreen;
    public static Base instance; //Classe estática


    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de uma Base");
            return;
        }

        instance = this;
    }

    void Start(){
        hpIni = hp;
    }

    void Update() {
        hpText.text = hp.ToString();    
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
            Defeat();
    }

    public void Defeat()
    {
        Debug.Log("PErDi");
         EndScreen.SetActive(true);
         EndScreen.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void Won(){
        EndScreen.SetActive(true);
         EndScreen.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) 
    {
        Virus virus = other.gameObject.transform.parent.GetComponent<Virus>();
    
        TakeDamage(virus.damage);
    }

    public void BaseLocate(Vector3 locate)
    {
        transform.position = locate;
    }

    public int getHP(){
        return hp;
    }
    public int getIHP(){
        return hpIni;
    }
}
