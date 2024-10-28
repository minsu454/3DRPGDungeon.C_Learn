using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItemSO", menuName = "SO/Item/CreateConsumableSO")]
public class ConsumableItemSO : BaseItemSO
{
    [Header("Consumable")]
    public int maxStack;
    public List<ItemDataConsumbale> consumableList;
}