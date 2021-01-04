using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScripte : MonoBehaviour
{

    public Slider sliderA;

    int hp;
    int hpFull;


    void Start()
    {
        hp = 100;
        hpFull = 100;
    }

    // Update is called once per frame
    void Update()
    {
        sliderA.value = (float)hp / hpFull;
    }

    public void OnClickButton()
    {
        hp = hp - 1;
    }
}