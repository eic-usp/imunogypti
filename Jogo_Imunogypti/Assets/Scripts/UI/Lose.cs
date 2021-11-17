using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void DoLittleStars(float Q){
        
        if(Q==null){
            return;
        }

        Debug.Log("Q:"+Q);

        for(int i=0;i<Mathf.Round(Q*3);i++){
            Stars[i].transform.GetComponent<SpriteRenderer>().sprite = goldStar;
            Stars[i].transform.GetComponent<LittleStars>().filled = true;
        }
    }

}
