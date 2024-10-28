using UnityEngine;

[CreateAssetMenu(fileName = "BaseItemSO", menuName = "SO/Item/CreateItemSO", order = 0)]
public class BaseItemSO : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
}
