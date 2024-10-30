using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour, IUnitParts
{
    [SerializeField] private Transform equipParent;     //장착할 물건에 부모 Transform
    public Equip CurEquip { get; private set; }         //현재 장착한 물건

    public UIInventory uiInventory;                     //ui인벤토리

    private PlayerController controller;                //플레이어 컨트롤러
    private PlayerCondition condition;                  //플레이어 상태

    public void OnInit(IUnitCommander commander)
    {
        Player player = commander as Player;
        controller = player.controller;
        condition = player.condition;
    }

    /// <summary>
    /// 인벤토리 키 입력 event
    /// </summary>
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

    /// <summary>
    /// 아이템 사용 키 입력 event
    /// </summary>
    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && CurEquip != null)
        {
            UseItem();
        }
    }

    /// <summary>
    /// 아이템 사용하는 함수
    /// </summary>
    public void UseItem()
    {
        CurEquip.OnUse(uiInventory);
    }

    /// <summary>
    /// 인벤토리에 어떤 것을 골랐는지 알려주는 함수
    /// </summary>
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

    /// <summary>
    /// 아이템 장착 함수
    /// </summary>
    public void EquipItem(string itemName)
    {
        GameObject equip = Managers.Addressable.LoadData<GameObject>($"Equip_{itemName}");

        CurEquip = Instantiate(equip, equipParent).GetComponent<Equip>();

        CurEquip.Init(itemName, controller, condition);
    }

    /// <summary>
    /// 아이템 가져오는 함수
    /// </summary>
    public bool TakeItem(string name, out bool isEquipped)
    {
        return uiInventory.TakeItem(name, out isEquipped);
    }
}
