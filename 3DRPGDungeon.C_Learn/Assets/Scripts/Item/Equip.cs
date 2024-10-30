using System.Runtime.CompilerServices;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public string itemName = string.Empty;          //아이템 이름 저장 변수

    protected PlayerController controller { get; private set; }     //플레이어 컨트롤러
    protected PlayerCondition condition { get; private set; }       //플레이어 상태

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public virtual void Init(string name, PlayerController controller, PlayerCondition condition)
    {
        itemName = name;
        this.controller = controller;
        this.condition = condition;
    }

    /// <summary>
    /// 사용할 때 실행할 함수
    /// </summary>
    public virtual void OnUse(UIInventory inventory)
    {
        inventory.CurItemRemove(out bool isDelete);

        if (isDelete)
            gameObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        itemName = string.Empty;
        Destroy(gameObject);
    }
}
