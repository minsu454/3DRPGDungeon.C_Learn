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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null)
        {
            curEquip.OnUse();
            //uiInventory.
        }
    }

    public void EquipNew()
    {

    }

    private void ChoiceItemKey(int key)
    {
        BaseItemSO equipItem = uiInventory.SetCursor(key);

        if (curEquip != null)
        {
            if (curEquip.item == equipItem)
                return;

            Destroy(curEquip.gameObject);
        }

        if (equipItem == null)
            return;

        GameObject equip = Managers.Addressable.LoadItem<GameObject>($"Equip_{equipItem.name}");
        curEquip = Instantiate(equip, equipParent).GetComponent<Equip>();
        curEquip.item = equipItem;
    }

    public bool TakeItem(string name)
    {
        return uiInventory.TakeItem(name);
    }
}
