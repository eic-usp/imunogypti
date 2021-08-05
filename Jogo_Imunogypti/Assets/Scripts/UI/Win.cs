using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Win : MonoBehaviour
{
    public int fHP=1;// vida final
    public int iHP=2;// vida inicial
    int R;
    public List<GameObject> Stars; //Estrelas
    [SerializeField] private GameObject StarBonus;
    float h; //Altura total do fluido dourado
    float K;
    float RBonus;
    private int sceneIndex;


    int c=0;
    public Text Points; //Texto com a pontuação


    public AudioSource starSong;
    public AudioClip magicSpell;

    public Sprite goldStar; //Sprite da estrela preenchida


    void Awake()
    {
        //CalculateHeights(fHP,iHP);
        //Debug.Log(((float)iHP/fHP).ToString());
        fHP = LifeManager.instance.getHP();
        iHP = LifeManager.instance.getIHP();
        R = (int) Mathf.Round(((float)fHP/(float)iHP)*3);
        
        sceneIndex = (SceneManager.GetActiveScene()).buildIndex;
        SaveLoader.saveFile.stagesWon[sceneIndex-2] = true;
        SaveLoader.saveFile.stars[sceneIndex-2] = R;
        SaveLoader.SaveGame();
        
        DoLittleStars(R);
        RBonus = (ImmunityManager.instance.getImmunity())/200f;
    }

    // Update is called once per frame
    void Update()
    {       /*
            //Fluido da estrela bonus e efeitos de particulas dos dois lados da estrela
            GameObject fluidStar= Stars[0].transform.GetChild(0).GetChild(0).gameObject;
            RectTransform effect = Stars[0].transform.GetChild(1).gameObject.GetComponent<RectTransform>();
            RectTransform effect2 = Stars[0].transform.GetChild(2).gameObject.GetComponent<RectTransform>();

            //Colliders e transform do fluido e da esrela bonus
            RectTransform rtFluidStar = fluidStar.GetComponent<RectTransform>();
            BoxCollider2D fluidStarCollider  = fluidStar.GetComponent<BoxCollider2D>();
            BoxCollider2D starCollider = Stars[0].transform.GetChild(0).GetComponent<BoxCollider2D>();
            
            if(h>0){
                if(!fluidStarCollider.bounds.Contains(starCollider.bounds.max)){
                    rtFluidStar.Translate(new Vector3(0,18*Time.deltaTime,0));
                    effect.Translate(new Vector3(0,18*Time.deltaTime,0));
                    effect2.Translate(new Vector3(0,18*Time.deltaTime,0));
                    h -= (5*K/R)*Time.deltaTime;
                    starSong.PlayOneShot(magicSpell,0.1f);
                }    
            }*/

            foreach(GameObject star in Stars){
                RectTransform rt = (RectTransform)star.transform;
                if(star.transform.GetComponent<LittleStars>().filled == true){
                   //Debug.Log("tchau");
                    rt.Rotate(new Vector3(0,0,100f*Time.deltaTime));
                }
            }

            if(RBonus>0){
                StarBonus.transform.GetComponent<SVGImage>().sprite = goldStar;
                Points.text = (RBonus * 100).ToString() + "%";
            }
            

    }

    /*public void CalculateHeights(int fHP,int iHP){

        if(fHP==null || iHP == null){
          return;
        }

        R = ((float)fHP/iHP);
        float H = 0;
        float Hl = 0;
        float Hg = 0;
        h = 0;
        int i=0;

        foreach(GameObject star in Stars){
            RectTransform rt = (RectTransform)star.transform;
            Hl = rt.rect.height * rt.localScale.y;
            if(i==0){
                Hg = Hl;
            }
            H += Hl;
            if(i<Stars.Count)
                i++;
        }
        h = R * H *100;
        Debug.Log(R);
        DoLittleStars(Hl,Hg);

    }*/

    public void DoLittleStars(int Q){
        
        if(Q==null){
            return;
        }

        Debug.Log("Q:"+Q);

        for(int i=0;i<Q;i++){
            Stars[i].transform.GetComponent<SVGImage>().sprite = goldStar;
            Stars[i].transform.GetComponent<LittleStars>().filled = true;
        }
        //Debug.Log("Subtract " +(int)Q *100 * Hl);
        //h = h - (int)Q * 100 *Hl;
        //Debug.Log("h final " + h);
        //Points.text = ((int)(Q)).ToString()+"%";
    }

    public void Play(){
        SceneManager.LoadScene("BaseLevel");
    }
    public void Return(){
        SceneManager.LoadScene("MainMenu");
    }
    
}
