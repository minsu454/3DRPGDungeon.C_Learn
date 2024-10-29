using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private UIItemSlot curEquipItem;

    [SerializeField] private List<UIItemSlot> slotList;

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

    public bool IsEquipped(UIItemSlot slot)
    {
        if (slot == null)
            return false;

        return slot.Equipped;
    }

    public void UseItem(out bool isDelete)
    {
        curEquipItem.Remove(out isDelete);
    }

    public string SetCursor(int key)
    {
        if (curEquipItem != null)
            curEquipItem.Equipped = false;

        curEquipItem = slotList[key];
        curEquipItem.Equipped = true;

        return curEquipItem.itemName;
    }
}
