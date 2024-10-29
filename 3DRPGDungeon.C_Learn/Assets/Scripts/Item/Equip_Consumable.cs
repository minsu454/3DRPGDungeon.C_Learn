using System.Collections;
using UnityEngine;

public class Equip_Consumable : Equip
{
    public override void OnUse(UIInventory inventory)
    {
        ConsumableItemSO slot = inventory.CurEquipItem.Item as ConsumableItemSO;
        inventory.CurItemRemove(out bool isDelete);

        ApplyItem(slot);

        if (isDelete)
            gameObject.SetActive(false);
    }

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
                    controller.MoveBoost(data.Value, data.Duration);
                    break;
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Destroy(gameObject);
    }
}