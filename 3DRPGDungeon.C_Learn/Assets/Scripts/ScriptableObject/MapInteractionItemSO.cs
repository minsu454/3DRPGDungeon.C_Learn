using UnityEngine;

/// <summary>
/// 맵상호작용아이템 SO
/// </summary>
[CreateAssetMenu(fileName = "MapInteractionItemSO", menuName = "SO/Item/MapInteractionItemSO", order = 2)]
public class MapInteractionItemSO : BaseItemSO
{
    [Header("MapInteraction")]
    public string needItemName;             //상호작용할 때 필요한 아이템 이름
    public string needItemDescription;      //상호작용할 때 필요한 설명
}