using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    /*
    public Text message1;

    // Start is called before the first frame update
    void Start()
    {
        message1.canvasRenderer.SetAlpha(1.0f);

        fadeout();
    }

    // Update is called once per frame
    void fadeout()
    {
        message1.CrossFadeAlpha(0, 3, false);
    }
    
    
    */
        
    public Text message;
    float fades = 0.0f;
    float time = 0;

    void Start()
    {
       
    }

    void Update()
    {
        time += Time.deltaTime;
        if(fades<=0.0f && time>=3.0f)
        {
            fades += 0.1f;
            message.color = new Color(0, 0, 0, fades);
            time = 0;
        }

        else if(fades>0.0f)
        {
            time = 0;
        }
        
    }
    

    
}
