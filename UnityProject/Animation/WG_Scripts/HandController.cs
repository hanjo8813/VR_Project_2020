using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private Animator anim;

    private float dirX;
    private float dirZ;
    private float keyE;
    private float towel;
    private float use;
    private float fakeOne;
    private float fireEx;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        dirZ = Input.GetAxis("Vertical");
        keyE = Input.GetAxis("E_KEY");
        towel = Input.GetAxis("Towel");
        use = Input.GetAxis("Use");
        fakeOne = Input.GetAxis("WetTowel");
        fireEx = Input.GetAxis("FireExtinguisher");

        anim.SetFloat("dirX", dirX);
        anim.SetFloat("dirZ", dirZ);
        anim.SetFloat("eKey", keyE);
        anim.SetFloat("rKey", use);
        anim.SetFloat("1Key", towel);
        anim.SetFloat("2Key", fireEx);
        anim.SetFloat("fake1Key", fakeOne);
        
    }
}  