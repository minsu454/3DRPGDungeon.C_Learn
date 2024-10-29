using UnityEngine;

[CreateAssetMenu(fileName = "MapInteractionItemSO", menuName = "SO/Item/MapInteractionItemSO", order = 2)]
public class MapInteractionItemSO : BaseItemSO
{
    [Header("MapInteraction")]
    public string needItemName;
    public string needItemDescription;
}