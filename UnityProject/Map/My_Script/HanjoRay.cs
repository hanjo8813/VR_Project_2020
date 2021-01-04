using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HanjoRay : MonoBehaviour
{
    public static double hp;
    public static double Score_smoke;
    public static double Score_fire;

    public bool UI_water_check;
    public bool HP_smoke_check;
    public bool UI_firefighter_check1;
    public bool UI_firefighter_check2;
    public bool UI_firefighter_check3;
    public bool UI_towel_check;
    public bool UI_call_check;
    public bool UI_call_check2;


    public float time;
    public float time2;
    public float time3;
    public float time4;
    public float time5;
    public float time6;
    public float time7;


    //레이어마스크
    [SerializeField] private LayerMask layer_watercooler;
    [SerializeField] private LayerMask layer_smoke;
    [SerializeField] private LayerMask layer_towel;
    [SerializeField] private LayerMask layer_fireex;
    [SerializeField] private LayerMask layer_firedmg;
    [SerializeField] private LayerMask layer_firefighter;
    [SerializeField] private LayerMask layer_door1;
    [SerializeField] private LayerMask layer_door2;
    [SerializeField] private LayerMask layer_escape;
    [SerializeField] private LayerMask layer_call;


    //이벤트UI
    public GameObject UI_watercooler = null;
    public GameObject UI_smoke = null;
    public GameObject UI_towelgauge = null;
    public GameObject UI_fireexgauge = null;
    public GameObject UI_callgauge = null;
    public GameObject UI_firedmg = null;
    public GameObject UI_firefighter_1 = null;
    public GameObject UI_firefighter_2 = null;
    public GameObject UI_towelcheck = null;
    public GameObject UI_call = null;
    public GameObject UI_call2 = null;


    //다른 객체
    public GameObject waterdrop = null;
    public GameObject towel = null;
    public GameObject fireex = null;
    public GameObject fire_ex_injection = null;
    public GameObject fire1 = null;
    public GameObject fire2 = null;
    public GameObject fire3 = null;
    public GameObject door1_close = null;
    public GameObject door1_open = null;
    public GameObject door2_close = null;
    public GameObject door2_open = null;

    // 시작시 초기화
    void Start()
    {
        hp = 100.0f;
        Score_smoke = 0.0f;
        Score_fire = 0.0f;

        time = 0;
        time2 = 0;
        time3 = 0;
        time4 = 0;
        time5 = 0;
        time6 = 0;
        time7 = 0;

        UI_water_check = false;
        HP_smoke_check = false;
        UI_firefighter_check1 = true;
        UI_firefighter_check2 = true;
        UI_firefighter_check3 = true;
        UI_towel_check = false;
        UI_call_check = true;
        UI_call_check2 = true;
    }

    void Update()
    {
        RaycastHit hitinfo;

        if(hp<=0)
        {
            SceneManager.LoadScene(9);
        }

        //------------------------------------------------------------------------------------------------------------------------------
        // 정수기 앞
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_watercooler))
        {
            //물줄기 나옴
            waterdrop.gameObject.SetActive(true);
            //e키 눌렀을때 
            if (Input.GetKey(KeyCode.E))
            {
                UI_water_check = true;
            }
            //e키 누르고 나면 UI제거
            if (UI_water_check == true)
            {
                UI_watercooler.gameObject.SetActive(false);
            }
            else
            {
                UI_watercooler.gameObject.SetActive(true);
            }
        }
        //발판 안밟았을때
        else
        {
            waterdrop.gameObject.SetActive(false);
            UI_watercooler.gameObject.SetActive(false);
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //false일때 물수건 안쓴거 true일때 쓴거
        if (HP_smoke_check == false && Input.GetKeyDown(KeyCode.R))
        {
            HP_smoke_check = true;
        }
        else if (HP_smoke_check == true && Input.GetKeyDown(KeyCode.R))
        {
            HP_smoke_check = false;
        }

        //연기복도발판
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_smoke))
        {
            if (HP_smoke_check == false)
            {
                UI_smoke.gameObject.SetActive(true);
                hp -= Time.deltaTime * 8;
                Score_smoke += Time.deltaTime * 8;

                Debug.Log(Score_smoke);
            }
        }
        else
        {
            UI_smoke.gameObject.SetActive(false);
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //수건집기
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_towel))
        {
            if (Input.GetKey(KeyCode.E))
            {

                UI_towelgauge.gameObject.SetActive(true);
                time += Time.deltaTime;
                if (time > 2.0f)
                {
                    UI_towelgauge.SetActive(false);
                    towel.gameObject.SetActive(false);
                    UI_towel_check = true;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //소화기 집기
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_fireex))
        {
            if (Input.GetKey(KeyCode.E))
            {
                UI_fireexgauge.gameObject.SetActive(true);
                time2 += Time.deltaTime;
                if (time2 > 2.0f)
                {
                    UI_fireexgauge.SetActive(false);
                    fireex.gameObject.SetActive(false);
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //불 데미지
        if (UI_firefighter_check2 == true)
        {
            if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_firedmg))
            {

                UI_firedmg.gameObject.SetActive(true);
                hp -= Time.deltaTime * 16;
                Score_fire += Time.deltaTime * 16;

                Debug.Log(Score_fire);
            }
            else
            {
                UI_firedmg.gameObject.SetActive(false);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //불 끄기
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_firefighter))
        {
            if (Input.GetKey(KeyCode.E))
            {
                UI_firefighter_check1 = false;
            }
            if (Input.GetKey(KeyCode.R))
            {
                UI_firefighter_check2 = false;
            }

            if (UI_firefighter_check1 == true)
            {
                UI_firefighter_1.gameObject.SetActive(true);
            }
            // E키 누르면
            else
            {
                UI_firefighter_1.gameObject.SetActive(false);
                UI_firefighter_2.gameObject.SetActive(true);
            }

            if (UI_firefighter_check2 == false)
            {
                UI_firefighter_2.gameObject.SetActive(false);
                fire_ex_injection.gameObject.SetActive(true);

                time3 += Time.deltaTime;
                if (time3 > 1.0f)
                {
                    fire1.SetActive(false);
                }
                if (time3 > 3.0f)
                {
                    fire2.SetActive(false);
                }
                if (time3 > 5.0f)
                {
                    fire3.SetActive(false);
                    fire_ex_injection.gameObject.SetActive(false);
                    UI_firedmg.gameObject.SetActive(false);
                    UI_firefighter_check2 = false;
                }
            }
        }
        else
        {
            UI_firefighter_1.gameObject.SetActive(false);
            UI_firefighter_2.gameObject.SetActive(false);
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //1번 문
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_door1))
        {
            if (Input.GetKey(KeyCode.E))
            {
                time4 += Time.deltaTime;
                if (time4 > 1.0f)
                {
                    door1_close.gameObject.SetActive(false);
                    door1_open.gameObject.SetActive(true);
                }
            }
            if (UI_towel_check == false)
            {
                UI_towelcheck.gameObject.SetActive(true);
            }
            else
            {
                UI_towelcheck.gameObject.SetActive(false);
            }
        }
        else
        {
            UI_towelcheck.gameObject.SetActive(false);
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //2번 문
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_door2))
        {
            if (Input.GetKey(KeyCode.E))
            {
                time5 += Time.deltaTime;
                if (time5 > 1.0f)
                {
                    door2_close.gameObject.SetActive(false);
                    door2_open.gameObject.SetActive(true);
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //2번 문 나오고 신고하기
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_call))
        {
            if (UI_call_check == true)
            {
                UI_call.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.E))
            {
                time6 += Time.deltaTime;
                UI_callgauge.gameObject.SetActive(true);
                UI_call.SetActive(false);
                if (time6 > 2.0f)
                {
                    UI_call_check = false;
                    UI_callgauge.SetActive(false);
                    UI_call2.SetActive(true);
                    UI_call_check2 = false;

                }
            }
            if (UI_call_check2 == false)
            {
                time7 += Time.deltaTime;
                if (time7 > 5.0f)
                {
                    UI_call2.SetActive(false);
                }
            }
        }
        else
        {
            UI_call.gameObject.SetActive(false);
            UI_call2.gameObject.SetActive(false);
        }
        //------------------------------------------------------------------------------------------------------------------------------
        // 탈출 발판 밟았을때
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hitinfo, 10f, layer_escape))
        {
            SceneManager.LoadScene(8);
        }

    }
}