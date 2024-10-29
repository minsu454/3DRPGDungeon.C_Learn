using Common.CoTimer;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUnitParts, IMapInteractionUnit
{
    [Header("Movement")]
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayerMask;
    private bool isJump = false;
    private const float jumpCost = 1f;
    private Rigidbody myRb;
    private float speed = 5;
    private Vector2 moveDir;

    [Header("Camera")]
    [SerializeField] private List<Camera> cameraList;
    private int showCameraIndex = 0;

    [Header("Look")]
    [SerializeField] private Transform camerasTr;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    [SerializeField] private float lookSensitivity = 0.05f;

    private Vector2 mouseDelta;
    private float camCurXRot;
    private bool canLook = true;

    private float moveDoping = 1;
    private float jumpDoping = 1;
    private HashSet<Coroutine> timerHashSet = new HashSet<Coroutine>();

    private IUnitCommander commander;
    private PlayerCondition condition;

    public void OnAwake(IUnitCommander commander)
    {
        myRb = GetComponent<Rigidbody>();
        ResetCamera();

        this.commander = commander;

        Player player = commander as Player;

        condition = player.condition;
        condition.DieEvent += OnDie;

        OnRespawn();
    }

    private void ResetCamera()
    {
        cameraList[showCameraIndex].depth = -10;
        showCameraIndex = 0;
        cameraList[showCameraIndex].depth = 1;
    }

    private void NextCamera()
    {
        cameraList[showCameraIndex].depth = -10;

        showCameraIndex++;
        showCameraIndex %= cameraList.Count;

        cameraList[showCameraIndex].depth = 1;
    }

    public void OnRespawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        commander.FixedUpdateEvent += OnFixedUpdate;
        commander.LateUpdateEvent += OnLateUpdate;
    }

    public void OnDie()
    {
        commander.FixedUpdateEvent -= OnFixedUpdate;
        commander.LateUpdateEvent -= OnLateUpdate;

        Cursor.lockState = CursorLockMode.None;
    }

    private void OnFixedUpdate()
    {
        Move();
        Jump();
    }

    private void OnLateUpdate()
    {
        if (!canLook)
            return;

        Look();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        dir *= (speed * moveDoping);
        dir.y = myRb.velocity.y;

        myRb.velocity = dir;
    }

    private void Jump()
    {
        if (!isJump)
            return;

        if (!IsGrounded())
            return;

        condition.TakeStamina(jumpCost);
        AddImpulseForce(Vector3.up, jumpPower * jumpDoping);
    }

    private void Look()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        camerasTr.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

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

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

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

    public void OnCameraChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            NextCamera();
        }
    }

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

    public void AddImpulseForce(Vector3 dir, float power)
    {
        myRb.AddForce(dir * power, ForceMode.Impulse);
    }

    public void MoveBoost(float value, float duration)
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

    public void JumpBoost(float value, float duration)
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
