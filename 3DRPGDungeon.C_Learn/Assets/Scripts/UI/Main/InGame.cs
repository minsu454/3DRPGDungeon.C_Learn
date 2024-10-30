using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGame : BaseUI
{
    [SerializeField] private UICondition health;                            //체력바
    [SerializeField] private UICondition stamina;                           //기력바
    [SerializeField] private UIInventory inventory;                         //인벤토리
    [SerializeField] private TextMeshProUGUI InfoCommentText;               //아이템 설명 Text
    [SerializeField] private TextMeshProUGUI MapItemInteractionCommentText; //맵아이템 상호작용 설명 Text
    [SerializeField] private UIDamageIndictor damageIndictor;               //손상 표시기

    public override void Init()
    {
        base.Init();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.condition.health = health;
        player.condition.stamina = stamina;
        player.equipment.uiInventory = inventory;
        player.interaction.InfoCommentText = InfoCommentText;
        player.interaction.MapItemInteractionCommentText = MapItemInteractionCommentText;
        player.condition.TakeDamageEvent += damageIndictor.Flash;

        health.Init();
        stamina.Init();
    }
}
