using System.Collections;
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
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
            Defeat();

        if(((float)hp/(float)hpIni)*100f<=30f)
            Fever();

        
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
        Debug.Log(hp.ToString() + hpIni.ToString() );
        isWithFever = true;
        FeverScreen.SetActive(true);
        //_vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value,Math.Cos(0.5.Time.deltaTime),0.5f*Time.deltaTime);
    }
}
