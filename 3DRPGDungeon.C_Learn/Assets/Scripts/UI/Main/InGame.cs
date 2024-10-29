using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGame : BaseUI
{
    [SerializeField] private UICondition health;
    [SerializeField] private UICondition stamina;
    [SerializeField] private UIInventory inventory;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private UIDamageIndictor damageIndictor;

    public override void Init()
    {
        base.Init();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.condition.health = health;
        player.condition.stamina = stamina;
        player.equipment.uiInventory = inventory;
        player.interaction.PromptText = promptText;
        player.condition.TakeDamageEvent += damageIndictor.Flash;

        health.Init();
        stamina.Init();
    }
}
