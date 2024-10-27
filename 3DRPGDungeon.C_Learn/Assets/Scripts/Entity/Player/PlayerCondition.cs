using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IUnitParts, IDamagable
{
    public UICondition health;
    public UICondition stamina;

    public void OnAwake(IUnitCommander commander)
    {
        commander.UpdateEvent += OnUpdate;
    }

    public void OnUpdate()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health.Subtract(damage);
    }
}
