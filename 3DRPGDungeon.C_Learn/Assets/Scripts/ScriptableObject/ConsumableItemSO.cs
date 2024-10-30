using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 소모품 아이템 SO
/// </summary>
[CreateAssetMenu(fileName = "ConsumableItemSO", menuName = "SO/Item/CreateConsumableSO", order = 1)]
public class ConsumableItemSO : BaseItemSO
{
    [Header("Consumable")]
    public int maxStack;                            //인벤토리에 최대 겹쳐놓을 수 있는 수
    public List<ItemConsumableData> consumableList; //소모품 아이템이 줄 능력 리스트
}
