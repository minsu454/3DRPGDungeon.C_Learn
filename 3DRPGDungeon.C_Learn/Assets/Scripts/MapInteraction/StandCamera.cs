using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandCamera : MapInteraction, IMapItemInteraction
{
    public bool EqualsItem(string itemName)
    {
        if (itemSO.needItemName == itemName)
            return true;

        return false;
    }

    public void Select()
    {
        Debug.Log("건전지가 충전되었습니다.");
    }
}
