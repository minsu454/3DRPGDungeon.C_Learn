using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour, IUnitParts
{
    [SerializeField] private Transform equipParent;

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

    private void ChoiceItemKey(int key)
    {
        uiInventory.SetCursor(key);
    }

    public bool TakeItem(string name)
    {
        return uiInventory.TakeItem(name);
    }
}
