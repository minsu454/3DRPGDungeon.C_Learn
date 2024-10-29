using System.Collections;
using UnityEngine;

public class Equip_Consumable : Equip
{

    public override void OnUse(UIInventory inventory)
    {
        inventory.UseItem(out bool isDelete);

        if(isDelete)
            gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        Destroy(gameObject);
    }
}