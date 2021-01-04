using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HP : MonoBehaviour
{
    Text myHP;
    int textHP;
    void Awake()
    {
        myHP = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textHP = (int)HanjoRay.hp;
        myHP.text = textHP.ToString();
    }
}
