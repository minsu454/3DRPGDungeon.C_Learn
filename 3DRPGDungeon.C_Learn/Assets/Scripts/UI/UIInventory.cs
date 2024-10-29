using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItemSlot CurEquipItem { get; private set; }

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

    public void CurItemRemove(out bool delete)
    {
        CurEquipItem.Remove(out delete);
    }

    public string SetCursor(int key)
    {
        if (CurEquipItem != null)
            CurEquipItem.Equipped = false;

        CurEquipItem = slotList[key];
        CurEquipItem.Equipped = true;

        return CurEquipItem.ItemName;
    }
}
