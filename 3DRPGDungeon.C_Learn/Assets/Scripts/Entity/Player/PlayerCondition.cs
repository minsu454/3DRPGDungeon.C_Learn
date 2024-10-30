using Common.CoTimer;
using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IUnitParts, IDamagable
{
    public UICondition health;              //플레이어 hp바
    public UICondition stamina;             //플레이어 기력바

    private bool isInvincibility = false;   //무적 상태 체크 변수
    public event Action TakeDamageEvent;    //데미지 줄 때 호출될 이벤트 저장 변수
    public event Action UseStaminaEvent;    //기력 사용할 때 호출될 이벤트 저장 변수
    public event Action RespawnEvent;       //리스폰 시 호출 이벤트 저장 변수
    public event Action DieEvent;           //죽을 시 호출 이벤트 저장 변수

    [SerializeField] private GameObject characterMesh;  //내 메쉬콜라이더 변수

    public void OnInit(IUnitCommander commander)
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

    /// <summary>
    /// 죽을 때 처리하는 함수
    /// </summary>
    public void Die()
    {
        characterMesh.SetActive(false);

        isInvincibility = true;
        DieEvent?.Invoke();
    }

    /// <summary>
    /// 체력 회복했을 때 함수
    /// </summary>
    public void Heal(float value)
    {
        health.Add(value);
    }

    /// <summary>
    /// 기력 사용했을 때 처리하는 함수
    /// </summary>
    public bool UseStamina(float value)
    {
        if (stamina.CurValue - value <= 0)
            return false;

        stamina.Subtract(value);
        UseStaminaEvent?.Invoke();
        return true;
    }

    /// <summary>
    /// 무적 부여하는 함수
    /// </summary>
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
