﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public int fHP=1;// numero total de inimigos mortos
    public int iHP=2;// numero total de inimigos
    float R; //razao
    public List<GameObject> Stars; //Estrelas

    public Sprite goldStar; //Sprite da estrela preenchida
    float h; //Altura total do fluido dourado

    void Awake()
    {
               
        Debug.Log(((float)iHP/fHP).ToString());

        fHP = LifeManager.instance.getHP();
        iHP = LifeManager.instance.getIHP();
        R = (((float)fHP/(float)iHP));
        DoLittleStars(R);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void DoLittleStars(float Q){
        
        if(Q==null){
            return;
        }

        Debug.Log("Q:"+Q);

        for(int i=0;i<Mathf.Round(Q*3);i++){
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
        this.transform.parent.gameObject.SetActive(false);
    }
    public void Return(){
        SceneManager.LoadScene("MainMenu");
        this.transform.parent.gameObject.SetActive(false);
    }

}
