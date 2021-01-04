using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class 사용하기 위해 추가합니다.

public class HP_gague : MonoBehaviour
{
    Slider slTimer;

    void Start()
    {
        slTimer = GetComponent<Slider>();
     
    }

    void Update()
    {
      
        if (slTimer.value > 0.0f)
        {
            // 시간이 변경한 만큼 slider Value 변경을 합니다.
            slTimer.value = (float)HanjoRay.hp;
        }
        else
        {
           // slTimer.value = 2.0f;
            //Debug.Log("Time is Zero.");
        }
    }
}
