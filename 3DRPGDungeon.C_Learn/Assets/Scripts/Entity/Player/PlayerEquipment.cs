using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour, IUnitParts
{
    [SerializeField] private Transform equipParent;
    private Equip curEquip;

    public UIInventory uiInventory;

    public void OnAwake(IUnitCommander commander)
    {
        
    }

    public void OnItemCursorKey(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (!int.TryParse(context.control.name, out int key))
                return;

            key--;

            ChoiceItemKey(key);
        }
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null)
        {
            curEquip.OnUse(uiInventory);
        }
    }

    private void ChoiceItemKey(int key)
    {
        string itemName = uiInventory.SetCursor(key);

        if (curEquip != null)
        {
            if (curEquip.name == itemName)
                return;

            Destroy(curEquip.gameObject);
        }

        if (itemName == string.Empty)
            return;

        EquipItem(itemName);
    }

    public void EquipItem(string itemName)
    {
        GameObject equip = Managers.Addressable.LoadItem<GameObject>($"Equip_{itemName}");
        curEquip = Instantiate(equip, equipParent).GetComponent<Equip>();
        curEquip.name = itemName;
    }

    public bool TakeItem(string name, out bool isEquipped)
    {
        return uiInventory.TakeItem(name, out isEquipped);
    }
}
