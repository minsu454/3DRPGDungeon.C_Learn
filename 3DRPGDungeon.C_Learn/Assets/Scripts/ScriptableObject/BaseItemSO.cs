using UnityEngine;

/// <summary>
/// 아이템 베이스 SO
/// </summary>
[CreateAssetMenu(fileName = "BaseItemSO", menuName = "SO/Item/CreateItemSO", order = 0)]
public class BaseItemSO : ScriptableObject
{
    [Header("Info")]
    public string displayName;      //아이템 이름
    public string description;      //아이템 설명
    public ItemType type;           //아이템 타입
}
