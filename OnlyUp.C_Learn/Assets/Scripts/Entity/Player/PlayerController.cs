using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUpdate
{
    private Rigidbody myRb;

    public int depth { get; private set; }

    private void Awake()
    {
        myRb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    void IUpdate.Connect()
    {
        
    }

    void IUpdate.OnUpdate()
    {
        
    }

    void IUpdate.OnFixedUpdate()
    {
        
    }
}
