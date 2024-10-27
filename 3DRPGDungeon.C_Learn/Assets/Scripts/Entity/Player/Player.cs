using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUnitCommander
{
    public PlayerController controller;
    public PlayerCondition condition;

    public event Action UpdateEvent;
    public event Action FixedUpdateEvent;
    public event Action LateUpdateEvent;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();

        controller.OnAwake(this);
        condition.OnAwake(this);
    }

    private void FixedUpdate()
    {
        FixedUpdateEvent?.Invoke();
    }

    private void Update()
    {
        UpdateEvent?.Invoke();
    }

    private void LateUpdate()
    {
        LateUpdateEvent?.Invoke();
    }
}
