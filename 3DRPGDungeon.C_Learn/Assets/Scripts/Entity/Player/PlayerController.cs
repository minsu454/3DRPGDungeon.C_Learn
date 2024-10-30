using Common.CoTimer;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUnitParts, IMapItemInteractionUnit
{
    [Header("Move")]
    private Rigidbody myRb;                                 //내 물리
    private float moveSpeed = 5;                            //움직일 때의 스피드 변수
    private Vector2 moveDir;                                //움직이는 방향 저장 변수
    [Header("Jump")]
    [SerializeField] private float jumpPower;               //점프 힘 변수
    [SerializeField] private LayerMask groundLayerMask;     //밟을 오브젝트의 레이어를 담아주는 변수
    private bool isJump = false;                            //점프 중인지 확인하는 변수
    private const float jumpCost = 1f;                      //점프 사용시 스테미너 사용 코스트 저장 상수

    [Header("Camera")]
    [SerializeField] private List<Camera> cameraList;       //내가 쓸 카메라 리스트
    private int showCameraIndex = 0;                        //카메라 리스트에 몇번 인덱스 사용중인지 저장하는 변수

    [Header("Look")]
    [SerializeField] private Transform camerasTr;           //카메라위치 저장 변수
    [SerializeField] private float minXLook;                //상하 움직임에 최소값 변수
    [SerializeField] private float maxXLook;                //상하 움직임에 최대값 변수
    [SerializeField] private float lookSensitivity = 0.05f; //고개 돌리는 속도 저장 변수
    private Vector2 mouseDelta;                             //마우스 델타값 저장 변수
    private float camCurXRot;                               //X로테이션 저장 변수

    private float moveDoping = 1;                           //도핑된 움직임 값 저장 변수
    private float jumpDoping = 1;                           //도핑된 점프 값 저장 변수

    private IUnitCommander commander;                       //이 유닛의 커맨더
    private PlayerCondition condition;                      //플레이어 상태

    public void OnInit(IUnitCommander commander)
    {
        myRb = GetComponent<Rigidbody>();
        ResetCamera();

        this.commander = commander;

        Player player = commander as Player;

        condition = player.condition;
        condition.DieEvent += OnDie;

        OnRespawn();
    }

    private void OnFixedUpdate()
    {
        Move();
        Jump();
    }

    private void OnLateUpdate()
    {
        Look();
    }

    public void AddImpulseForce(Vector3 dir, float power)
    {
        myRb.AddForce(dir * power, ForceMode.Impulse);
    }

    /// <summary>
    /// 1인칭 카메라로 리셋해주는 함수
    /// </summary>
    private void ResetCamera()
    {
        cameraList[showCameraIndex].depth = -10;
        showCameraIndex = 0;
        cameraList[showCameraIndex].depth = 1;
    }

    /// <summary>
    /// 다음 리스트에 들어있는 카메라 들고오는 함수
    /// </summary>
    private void NextCamera()
    {
        cameraList[showCameraIndex].depth = -10;

        showCameraIndex++;
        showCameraIndex %= cameraList.Count;

        cameraList[showCameraIndex].depth = 1;
    }

    /// <summary>
    /// 스폰되었을 때 호출 함수
    /// </summary>
    public void OnRespawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        commander.FixedUpdateEvent += OnFixedUpdate;
        commander.LateUpdateEvent += OnLateUpdate;
    }

    /// <summary>
    /// 죽었을 때 호출 함수
    /// </summary>
    public void OnDie()
    {
        commander.FixedUpdateEvent -= OnFixedUpdate;
        commander.LateUpdateEvent -= OnLateUpdate;

        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// 움직이는 함수
    /// </summary>
    private void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;

        float angle = CalculateNextFrameGroundAngle(dir, moveSpeed);
        Debug.Log(angle);

        dir = angle < 30 ? dir * moveSpeed * moveDoping : Vector3.zero;
        dir.y = myRb.velocity.y;
        
        myRb.velocity = dir;
    }

    /// <summary>
    /// 플레이어의 다음 움직임 부분에 레이를 쏴 경사각을 아는 함수
    /// </summary>
    private float CalculateNextFrameGroundAngle(Vector3 dir, float moveSpeed)
    {
        var nextFramePlayerPos = transform.position + dir * moveSpeed * Time.fixedDeltaTime;

        Debug.DrawRay(nextFramePlayerPos, Vector3.down, Color.red, 1f);

        if (Physics.Raycast(nextFramePlayerPos, Vector3.down, out RaycastHit hit, 1f, groundLayerMask))
            return Vector3.Angle(Vector3.up, hit.normal);

        return 0f;
    }

    /// <summary>
    /// 플레이어 점프 함수
    /// </summary>
    private void Jump()
    {
        if (!isJump)
            return;

        if (!IsGrounded())
            return;

        condition.UseStamina(jumpCost);
        AddImpulseForce(Vector3.up, jumpPower * jumpDoping);
    }

    /// <summary>
    /// 플레이어가 마우스 delta값이 움직일 때 그곳을 바라보게 설정해주는 함수
    /// </summary>
    private void Look()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        camerasTr.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// 움직이는 키 입력 event함수
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveDir = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            moveDir = Vector3.zero;
        }
    }

    /// <summary>
    /// 마우스 움직임 키 입력 event함수
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 점프 키 입력 event함수
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isJump = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isJump = false;
        }
    }

    /// <summary>
    /// 카메라 전환 event함수
    /// </summary>
    public void OnCameraChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            NextCamera();
        }
    }

    /// <summary>
    /// 지면에 레이를 쏴 점프가 가능한 상황인지 알려주는 함수
    /// </summary>
    private bool IsGrounded()
    {
        Ray[] ray = new Ray[4]
        {
            new Ray(transform.position + (Vector3.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (Vector3.back * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (Vector3.left * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (Vector3.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
        };

        Debug.DrawRay(transform.position + (Vector3.forward * 0.2f) + (transform.up * 0.01f), Vector3.down * 0.02f, Color.red, 5f);

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.02f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 움직임에 도핑해주는 함수
    /// </summary>
    public void SetMoveDoping(float value, float duration)
    {
        float plus = value / 10;

        moveDoping += plus;

        StartCoroutine(CoTimer.Start(duration, () =>
        {
            if (this == null)
                return;

            moveDoping -= plus;
        }));
    }

    /// <summary>
    /// 점프에 도핑해주는 함수
    /// </summary>
    public void SetJumpDoping(float value, float duration)
    {
        float plus = value / 10;
        jumpDoping += plus;

        StartCoroutine(CoTimer.Start(duration, () =>
        {
            if (this == null)
                return;

            jumpDoping -= plus;
        }));
    }
}
