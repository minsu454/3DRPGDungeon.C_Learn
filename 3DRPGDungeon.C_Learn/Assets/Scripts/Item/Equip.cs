using System.Runtime.CompilerServices;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public string itemName = string.Empty;

    protected PlayerController controller { get; private set; }
    protected PlayerCondition condition { get; private set; }

    public virtual void Init(string name, PlayerController controller, PlayerCondition condition)
    {
        itemName = name;
        this.controller = controller;
        this.condition = condition;
    }

    public virtual void OnUse(UIInventory inventory)
    {
        inventory.CurItemRemove(out bool isDelete);

        if (isDelete)
            gameObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        itemName = string.Empty;
        Destroy(gameObject);
    }
}
