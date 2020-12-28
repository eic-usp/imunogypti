using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateImunoglobulins : BaseNoAttack
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> ImunoglobPrefabs;
    [SerializeField] float fireCountdown = 0f;
    [SerializeField] float plotRate= 0.5f;

    public override void Apply(List<GameObject> targets){
        if(targets.Count != 0 && fireCountdown <= 0)
        {
            foreach (GameObject target in targets)
            {
                int num = UnityEngine.Random.Range(0,ImunoglobPrefabs.Count);
    			GameObject ImunoObj = Instantiate(ImunoglobPrefabs[num], target.transform);
    			Imunoglobulin imunoglob = ImunoObj.GetComponent<Imunoglobulin>();
                //Imunoglobulin imunoglobB = ImunoObjB.GetComponent<Imunoglobulin>();

                /*if(imunoglob != null)
                {
                    bullet.target = target;
                    bullet.damage = damage * buffDamage;
                }*/
            }
            fireCountdown = 1f / plotRate;
        }

        if(fireCountdown > 0)
            fireCountdown -= Time.deltaTime;
    }

    public override void Remove(List<GameObject> targets){}
}
