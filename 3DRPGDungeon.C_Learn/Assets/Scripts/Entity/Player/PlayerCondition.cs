using Common.CoTimer;
using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IUnitParts, IDamagable
{
    public UICondition health;
    public UICondition stamina;

    private bool isInvincibility = false;
    public event Action TakeDamageEvent;
    public event Action TakeStaminaEvent;
    public event Action RespawnEvent;
    public event Action DieEvent;

    [SerializeField] private GameObject characterMesh;

    public void OnAwake(IUnitCommander commander)
    {
        commander.UpdateEvent += OnUpdate;

        RespawnEvent?.Invoke();
    }

    public void OnUpdate()
    {
        stamina.Add(stamina.PassiveValue * Time.deltaTime);
    }

    public bool TakeDamage(float damage)
    {
        if (isInvincibility)
            return false;

        health.Subtract(damage);
        TakeDamageEvent?.Invoke();

        if (health.CurValue == 0)
            Die();

        return true;
    }

    public void Die()
    {
        characterMesh.SetActive(false);

        isInvincibility = true;
        DieEvent?.Invoke();
    }

    public void Heal(float value)
    {
        health.Add(value);
    }

    public bool TakeStamina(float value)
    {
        if (stamina.CurValue - value <= 0)
            return false;

        stamina.Subtract(value);
        TakeStaminaEvent?.Invoke();
        return true;
    }

    public void OnInvincibility(float duration)
    {
        isInvincibility = true;
        StartCoroutine(CoTimer.Start(duration, () =>
        {
            if (this == null)
                return;

            isInvincibility = false;
        }));
    }
}
