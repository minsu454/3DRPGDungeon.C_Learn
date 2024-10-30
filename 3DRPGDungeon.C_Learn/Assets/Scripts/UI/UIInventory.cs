using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItemSlot CurEquipItem { get; private set; }    //현재장착아이템

    [SerializeField] private List<UIItemSlot> slotList;     //아이템 슬롯 리스트

    /// <summary>
    /// 아이템을 인벤토리에 추가해주는 함수
    /// </summary>
    public bool TakeItem(string name, out bool isEquipped)
    {
        bool completed = false;
        isEquipped = false;

        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].IsPossible(name))
            {
                slotList[i].Add(name);
                completed = true;

                isEquipped = IsEquipped(slotList[i]);
                break;
            }
        }

        return completed;
    }

    /// <summary>
    /// 장착하고 있는 무기와 비교하는 함수
    /// </summary>
    public bool IsEquipped(UIItemSlot slot)
    {
        if (slot == null)
            return false;

        return slot.Equipped;
    }

    /// <summary>
    /// 현재 아이템 지워주는 함수
    /// </summary>
    public void CurItemRemove(out bool delete)
    {
        CurEquipItem.Remove(out delete);
    }

    /// <summary>
    /// 커서가 있는 아이템슬롯 설정하는 함수
    /// </summary>
    public string SetCursor(int key)
    {
        if (CurEquipItem != null)
            CurEquipItem.Equipped = false;

        CurEquipItem = slotList[key];
        CurEquipItem.Equipped = true;

        return CurEquipItem.ItemName;
    }
}
