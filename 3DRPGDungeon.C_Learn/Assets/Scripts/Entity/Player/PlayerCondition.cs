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

    public void TakeDamage(float damage)
    {
        if (isInvincibility)
            return;

        health.Subtract(damage);
        TakeDamageEvent?.Invoke();

        if (health.CurValue == 0)
            Die();
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

    public void TakeStamina(float value)
    {
        stamina.Subtract(value);
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
