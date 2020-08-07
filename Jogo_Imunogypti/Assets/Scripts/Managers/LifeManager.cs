﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

//classe que controla as principais funcoes do jogo
public class LifeManager : MonoBehaviour
{
    // [SerializeField] private Base[] bases;
    [SerializeField] private int hp;
    [SerializeField] private int hpIni;
    [SerializeField] private Text hpText;

    [SerializeField] private GameObject pProcessing;
    [SerializeField] private GameObject FeverScreen;
    private PostProcessVolume volume;
    private Vignette _vignette;

    public static LifeManager instance; //Classe estática
    public static ImmunityManager immunityManager;

    private bool defeat=false;
    private bool isWithFever=false;

    public GameObject EndScreen;

    private GameObject[] Linfocitos;

    [SerializeField] private Slider HidratationBar;
    private float elapsedTime = 0f;
    private float timeForDecrease = 2f;
    private float maxActualHidratation=0.4f;


    float t=Mathf.PI/5f;


    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("Mais de um LifeManager");
            return;
        }

        instance = this;
    }

    void Start()
    {
        //hpIni = hp;
        immunityManager = ImmunityManager.instance;

        //Ajustes iniciais na vignette
        volume = pProcessing.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out _vignette);
        _vignette.intensity.value = 0f;

    }

    void Update()
    {
        hpText.text = hp.ToString();
        if(isWithFever){
            //perde pontos de imunidade com o tempo
            if(immunityManager!=null)
                immunityManager.Decrease(5f*Time.deltaTime);

            //brilho variavel nas bordas
            t+=Time.deltaTime;
            _vignette.intensity.value =Mathf.Lerp(_vignette.intensity.value,5*Mathf.Cos(5f*t),0.5f*Time.deltaTime);
        }

        if(elapsedTime<timeForDecrease){
            HidratationBar.value = Mathf.Lerp(maxActualHidratation,maxActualHidratation - 0.02f*maxActualHidratation,(elapsedTime/timeForDecrease));
            elapsedTime+=Time.deltaTime;
            //Debug.Log(elapsedTime);
        }
        else{
            elapsedTime = 0f;
            maxActualHidratation = HidratationBar.value;
        }

        if(HidratationBar.value < 0.4){
            AttackAttributesManager.instance.buffLinfocito(-0.5f);
            AttackAttributesManager.instance.buffNeutrofilo(-0.5f,-0.5f);
            AttackAttributesManager.instance.buffMacrofago(-0.5f,-0.5f);

        }

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
            Defeat();

        if(((float)hp/(float)hpIni)*100f<=30f && isWithFever==false)
            Fever();

        maxActualHidratation -=0.1f*maxActualHidratation;
        elapsedTime = 0f;      
    }

    //funcao de derrota do jogador
    public void Defeat()
    {
        FeverScreen.SetActive(false);
        Debug.Log("perdeu");
        defeat = true;
        EndScreen.SetActive(true);
        EndScreen.transform.GetChild(1).gameObject.SetActive(true);
    }

    //funcao de vitoria do jogador
    public void Win()
    {
        FeverScreen.SetActive(false);
        if(defeat==false){
            Debug.Log("ganhou");
            EndScreen.SetActive(true);
            EndScreen.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public int getHP(){
        return hp;
    }
    
    public int getIHP(){
        return hpIni;
    }

    public void Fever(){

        isWithFever = true;
        FeverScreen.SetActive(true);

        foreach(GameObject sale in Shopping.instance.sales){
            if(sale.tag == "Linfocito"){
                Tower tower = sale.GetComponent<Tower>();
                tower.cost += 50;          
            }
        }

        AttackAttributesManager.instance.buffLinfocito(1.2f);
    }

    public void Hidratate(){
        maxActualHidratation+= 0.05f*maxActualHidratation;
    }
}