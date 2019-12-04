using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    public static Shopping instance;

    void Awake(){
        if(instance!=null){
            Debug.LogError("Mais de um Shopping");
            return;
        }

        instance = this;
    }

    BuildManager buildManager;
    public GameObject turret;
    public bool canDrag = false;

    //public Image priceBar;
    //public Text TowerPrice;
    float price;
    //public Text TowerName;

    PlayerAttributesController playattb;

    void Start()
    {
        buildManager = BuildManager.instance;
        playattb = PlayerAttributesController.instance;
        //priceBar.gameObject.SetActive(false);
    }


    void Update()
    {
        if(Input.GetMouseButton(0) && canDrag==true && turret!=null){
            if(buildManager.GetTurretToBuild()!=null){
                turret.transform.position = GetMouseWorldPos();
            }
        }
        else{
            if(canDrag==true && turret!=null){
                Debug.Log("Torre destruida");
                canDrag = false;
                Destroy(turret);
                playattb.setMoney(price);
                return;
            }
        }
    }


    /*public void Buy(){
        float price = float.Parse(TowerPrice.text);
        if(playattb.getMoney()>price){
            if(TowerName.text=="Neutrofilo"){
                buildManager.SetTurretToBuild(buildManager.Neutrofilo);
            }
            GameObject turretToBuild = buildManager.GetTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild,new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,0)));
            canDrag = true;

            playattb.setMoney(-price);
        
        }
    }
    */

    public void BuyNeutrofilo(){
         price = buildManager.getPrice("Neutrofilo");
        //TowerName = "Neutrofilo";
        if(playattb.getMoney()>price){
            buildManager.SetTurretToBuild(buildManager.Neutrofilo);
        }
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild,new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,0)));
        canDrag = true;

        playattb.setMoney(-price);

    }
    private Vector3 GetMouseWorldPos(){
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 15;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    public void SetTurretTransform(float Xo, float Yo, float Zo, float d){
        if(turret!=null){
        turret.transform.parent = Camera.main.transform;
        turret.transform.localPosition = new Vector3((d/Zo)*Xo,(d/Zo)*Yo,d);
        turret.transform.rotation = Quaternion.Euler(-30,0,0);
        }
    }
}
