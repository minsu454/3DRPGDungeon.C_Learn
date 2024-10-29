using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour, IUnitParts
{
    [SerializeField] private Transform equipParent;
    public Equip CurEquip { get; private set; }

    public UIInventory uiInventory;

    private PlayerController controller;
    private PlayerCondition condition;

    public void OnAwake(IUnitCommander commander)
    {
        Player player = commander as Player;
        controller = player.controller;
        condition = player.condition;
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
        if (context.phase == InputActionPhase.Performed && CurEquip != null)
        {
            CurEquip.OnUse(uiInventory);
        }
    }

    private void ChoiceItemKey(int key)
    {
        string itemName = uiInventory.SetCursor(key);

        if (CurEquip != null)
        {
            if (CurEquip.name == itemName)
                return;

            Destroy(CurEquip.gameObject);
        }

        if (itemName == string.Empty)
            return;

        EquipItem(itemName);
    }

    public void EquipItem(string itemName)
    {
        GameObject equip = Managers.Addressable.LoadItem<GameObject>($"Equip_{itemName}");
        CurEquip = Instantiate(equip, equipParent).GetComponent<Equip>();

        CurEquip.Init(controller, condition);
    }

    public bool TakeItem(string name, out bool isEquipped)
    {
        return uiInventory.TakeItem(name, out isEquipped);
    }
}
