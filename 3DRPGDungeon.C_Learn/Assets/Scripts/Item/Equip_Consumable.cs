using System.Collections;
using UnityEngine;

public class Equip_Consumable : Equip
{
    /// <summary>
    /// 사용할 때 실행할 함수
    /// </summary>
    public override void OnUse(UIInventory inventory)
    {
        ConsumableItemSO slot = inventory.CurEquipItem.Item as ConsumableItemSO;
        ApplyItem(slot);

        base.OnUse(inventory);
    }

    /// <summary>
    /// 소모품 능력들 적용시켜주는 함수
    /// </summary>
    private void ApplyItem(ConsumableItemSO slot)
    {
        foreach (var data in slot.consumableList)
        {
            switch (data.type)
            {
                case ConsumableType.Health:
                    condition.Heal(data.Value);
                    break;
                case ConsumableType.Stamina:

                    break;
                case ConsumableType.Invincibility:
                    condition.OnInvincibility(data.Duration);
                    break;
                case ConsumableType.MoveSpeed:
                    controller.SetMoveDoping(data.Value, data.Duration);
                    break;
            }
        }
    }
}