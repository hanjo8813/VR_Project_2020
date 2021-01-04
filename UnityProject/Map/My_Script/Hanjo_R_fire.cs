using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hanjo_R_fire : MonoBehaviour
{
    Text result_smokefire;

    double text_smoke;
    double text_fire;


    void Awake()
    {
        result_smokefire = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text_smoke = Math.Round(HanjoRay.Score_smoke,1);
        text_fire = Math.Round(HanjoRay.Score_fire,1);

        result_smokefire.text = "유독가스로 인한 감점 : " + text_smoke.ToString() +"\n"+ "화상으로 인한 감점 : " + text_fire.ToString();

    }
}
