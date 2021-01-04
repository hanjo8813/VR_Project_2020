using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hanjo_Result : MonoBehaviour
{
    Text result_hp;

    int text_hp;

    void Awake()
    {
        result_hp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text_hp = (int)HanjoRay.hp;
        result_hp.text = "최종점수 : " + text_hp.ToString();
        
    }
}
