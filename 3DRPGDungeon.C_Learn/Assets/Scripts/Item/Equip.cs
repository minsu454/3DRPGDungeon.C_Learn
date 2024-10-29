using UnityEngine;

public class Equip : MonoBehaviour
{
    public string itemName = string.Empty;

    public virtual void OnUse(UIInventory inventory)
    {

    }

    protected virtual void OnDisable()
    {
        itemName = string.Empty;
    }
}
