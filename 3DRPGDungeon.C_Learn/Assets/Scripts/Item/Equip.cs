using System.Runtime.CompilerServices;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public string itemName = string.Empty;

    protected PlayerController controller { get; private set; }
    protected PlayerCondition condition { get; private set; }

    public virtual void Init(PlayerController controller, PlayerCondition condition)
    {
        this.controller = controller;
        this.condition = condition;
    }

    public virtual void OnUse(UIInventory inventory)
    {

    }

    protected virtual void OnDisable()
    {
        itemName = string.Empty;
    }
}
