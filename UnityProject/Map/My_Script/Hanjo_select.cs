using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanjo_select : MonoBehaviour
{
    void Start()
    {
        Tutorial.walkSpeed = 12;
    }
    public void Click()
    {
        Tutorial.walkSpeed = 8;
    }
}
