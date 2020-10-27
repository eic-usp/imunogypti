using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmController : MonoBehaviour
{
    private float itimeScale = 1;

    public void ChangeRhythm(Text scaleChange) {
    	float change = 1.0f;;
    	switch(scaleChange.text){
            case "1x":
    			change = 2.0f;
                scaleChange.text = "2x";
    			break;
    		case "2x":
    			change = 1.0f;
                scaleChange.text = "1x";
    			break;
    		/*case "0.5x":
    			change = 0.5f;
                scaleChange.text = "1.5x";
    			break;
    		case "1.5x":
    			change = 1.5f;
                scaleChange.text = "2x";
    			break;
    		case "0x":
    			change = 0.0f;
                scaleChange.text = "0.5x";
    			break;*/
            default:
                change = 1.0f;
                break;

    	}

        Time.timeScale = itimeScale;
        Time.timeScale = Time.timeScale*change;
    }

    public void ReturnRhythm()
    {
        Time.timeScale = itimeScale;
    }
}
