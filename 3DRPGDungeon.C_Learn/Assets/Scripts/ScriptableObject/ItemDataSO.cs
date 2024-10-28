using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item/DataSO")]
public class ItemDataSO : ScriptableObject
{
    public GameObject go;
    public Sprite sprite;
    public ScriptableObject so;
}

