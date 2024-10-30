using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUnitCommander
{
    public PlayerController controller { get; private set; }        //플레이어 컨트롤러
    public PlayerCondition condition { get; private set; }          //플레이어 상태
    public PlayerEquipment equipment { get; private set; }          //플레이어 장비
    public PlayerInteraction interaction { get; private set; }      //플레이어 상호작용

    public event Action UpdateEvent;
    public event Action FixedUpdateEvent;
    public event Action LateUpdateEvent;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        equipment = GetComponent<PlayerEquipment>();
        interaction = GetComponent<PlayerInteraction>();

        controller.OnInit(this);
        condition.OnInit(this);
        equipment.OnInit(this);
        interaction.OnInit(this);
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
