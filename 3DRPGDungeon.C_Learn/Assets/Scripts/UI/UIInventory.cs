using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItemSlot CurEquipItem;

    [SerializeField] private List<UIItemSlot> slotList;

    public bool TakeItem(string name)
    {
        bool completed = false;

        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].IsPossible(name))
            {
                slotList[i].Set(name);
                completed = true;
                break;
            }
        }

        return completed;
    }

    public void SetCursor(int key)
    {
        if (slotList[key] == CurEquipItem)
            return;

        if (CurEquipItem != null)
            CurEquipItem.Equipped = false;


        CurEquipItem = slotList[key];
        CurEquipItem.Equipped = true;
    }
}
