using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동 스피드 조정 변수
    
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;

    // 얼마나 점프시킬지
    [SerializeField]
    private float jumpForce;

    // 상태 변수
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;


    // 앉았을 때 얼마나 앉을지 결정하는 변수
    private float crouchPosY = 1;
    private float originPosY;
    private float applyCrouchPosY;

    // 땅 착지여부 
    private CapsuleCollider capsuleCollider;

     
    // 회전 민감도
    [SerializeField]
    private float lookSensitivity;

    // 카메라 각도 limit
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    // 필요한 컴포넌트
    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;

    private Animator animator;

    private float moveDirX;
    private float moveDirZ;
    /// /////////////////////////////////////////////////////////////////////////
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();  //player가 capsuleCollider를 통제할 수 있도록
        myRigid = GetComponent<Rigidbody>();
        applySpeed = Tutorial.walkSpeed;

        //객체(캐릭터) 내 카메라 위치 초기화
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        CameraRotation();
        CharacterRotation();
    }
    ////////////////////////////////////////////////////////////////////////////
    
    //앉기 시도
    private void TryCrouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    //앉기 동작 실행
    private void Crouch()
    {
        isCrouch = !isCrouch;   //isCrouch 반전

        if(isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = Tutorial.walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    //부드러운 앉기 동작
    IEnumerator CrouchCoroutine()       //Coroutine >> 빠르게 왔다갔다하면서 병렬처리가 가능하게 해줌
    {
        float posY = theCamera.transform.localPosition.y;
        int count = 0;

        while(posY != applyCrouchPosY)
        {
            count++;
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.3f);     //보간을 이용해 자연스러움 추가, 숫자가 높을수록 빠르게 증가
            theCamera.transform.localPosition = new Vector3(0, posY, 0);      //x와 z는 변화가 없기때문에 객체 안에 실제 카메라 위치인 0을 대입
            
            // 계속해서 카메라 y값이 미세하게 바뀌는 것을 방지
            if (count > 15)
                break;

            yield return null;        // 한 프레임 대기
        }

        theCamera.transform.localPosition = new Vector3(0f, applyCrouchPosY, 0.24f);
    }

    //지면 체크
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);      //땅에 충돌했는지 검사, 0.1f는 약간의 여유
    }

    //점프 시도
    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            animator.Play("Jump", -1, 0);
        }
    }

    //점프 실행
    private void Jump()
    {
        //앉은 상태에서 점프 시 앉기를 해제
        if (isCrouch)
            Crouch();

        myRigid.velocity = transform.up * jumpForce;    // transform.up = (0,1,0)

    }

    //달리기 시도
    private void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    //달리기 실행
    private void Running()
    {
        //앉은 상태에서 달리기 시 앉기를 해제
        if (isCrouch)
            Crouch();

        isRun = true;
        applySpeed = runSpeed;
    } 

    //달리기 취소
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = Tutorial.walkSpeed;
    }

    //움직임 실행
    private void Move()
    {
        moveDirX = Input.GetAxisRaw("Horizontal");
        moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;
         
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);

        //애니메이션
        animator.SetFloat("h", moveDirX);
        animator.SetFloat("v", moveDirZ);
    }

    // 캐릭터 좌우 회전
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxis("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationY));
    }

    // 카메라 상하 회전
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;

        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
 
