﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private Text hpText;

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
         SceneManager.LoadScene("Lose");
    }

    private void OnTriggerEnter(Collider other) 
    {
        Virus virus = other.GetComponent<Virus>();
    
        TakeDamage(virus.damage);
    }

    public void BaseLocate(Vector3 locate)
    {
        transform.position = locate;
    }
}
