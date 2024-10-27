using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BaseUI
{
    [SerializeField] private UICondition health;
    [SerializeField] private UICondition stamina;

    public override void Init()
    {
        base.Init();

        PlayerCondition condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
        condition.health = health;
        condition.stamina = stamina;

        health.Init();
        stamina.Init();
    }
}
