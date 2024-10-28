using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUnitParts, IMapInteractionUnit
{
    [Header("Movement")]
    [SerializeField] private float jumpPower;
    [SerializeField] private bool isJump = false;
    [SerializeField] private LayerMask groundLayerMask;
    private Rigidbody myRb;
    private float speed = 5;
    private Vector2 moveDir;

    [Header("Camera")]
    [SerializeField] private List<Camera> cameraList;

    [Header("Look")]
    [SerializeField] private Transform camerasTr;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    [SerializeField] private float lookSensitivity = 0.05f;

    private Vector2 mouseDelta;
    private float camCurXRot;
    private bool canLook = true;

    public void OnAwake(IUnitCommander commander)
    {
        myRb = GetComponent<Rigidbody>();
        ResetCamera();

        Cursor.lockState = CursorLockMode.Locked;

        commander.FixedUpdateEvent += OnFixedUpdate;
        commander.LateUpdateEvent += OnLateUpdate;
    }

    private void ResetCamera()
    {
        cameraList[0].depth = 1;
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
        dir *= speed;
        dir.y = myRb.velocity.y;

        myRb.velocity = dir;
    }

    private void Jump()
    {
        if (!isJump)
            return;

        if (!IsGrounded())
            return;

        AddImpulseForce(Vector3.up, jumpPower);
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

        Debug.DrawRay(transform.position + (Vector3.forward * 0.2f) + (transform.up * 0.01f), Vector3.down * 0.01f, Color.red, 5f);

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.01f, groundLayerMask))
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
}
