using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class 사용하기 위해 추가합니다.
using UnityEngine.SceneManagement;
public class LOADING : MonoBehaviour
{
    Slider slTimer;
    float fSliderBarTime;
    bool getkey = true;
    void Start()
    {
        slTimer = GetComponent<Slider>();
        getkey = true;
        slTimer.value = 0;
    }

    void Update()
    {
        
        if (slTimer.value < 100.0f)
        {
            // 시간이 변경한 만큼 slider Value 변경을 합니다.
            slTimer.value += Time.deltaTime*20f;
        }
        else
        {
           
            SceneManager.LoadScene("Silver_Escape2");
        }
    }
}
