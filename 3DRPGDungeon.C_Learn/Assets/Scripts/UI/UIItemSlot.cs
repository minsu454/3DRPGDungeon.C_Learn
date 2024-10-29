using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIItemSlot : MonoBehaviour
{
    public string itemName { get; private set; } = string.Empty;
    private BaseItemSO item;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Outline outline;

    private int Quantity;

    private bool equipped = false;
    public bool Equipped
    {
        get { return equipped; }
        set
        {
            equipped = value;
            SetOutLine();
        }

    }

    public bool IsPossible(string name)
    {
        if (itemName == string.Empty)
            return true;

        if (itemName == name)
        {
            ConsumableItemSO temp = item as ConsumableItemSO;

            if (temp != null && Quantity < temp.maxStack)
            {
                return true;
            }
        }

        return false;
    }

    public void Add(string name)
    {
        if (itemName == string.Empty)
        {
            itemName = name;

            Managers.Addressable.LoadItemAsync<Sprite>(name, (sprite) =>
            {
                if (this == null)
                    return;

                if (itemName != name)
                    return;

                icon.sprite = sprite;
                icon.gameObject.SetActive(true);
                icon.DOFade(1.0f, 0.5f).From(0);
            });
            item = Managers.Addressable.LoadItem<BaseItemSO>(name);
        }

        Quantity++;
        quantityText.text = Quantity > 1 ? Quantity.ToString() : string.Empty;

        if (outline != null)
        {
            SetOutLine();
        }
    }

    public void Remove(out bool isDelete)
    {
        Quantity--;

        isDelete = false;

        if (Quantity == 0)
        {
            Clear();
            isDelete = true;
            return;
        }

        quantityText.text = Quantity > 1 ? Quantity.ToString() : string.Empty;
    }

    public void SetOutLine()
    {
        outline.enabled = equipped;
    }

    private void Clear()
    {
        itemName = string.Empty;
        icon.sprite = null;
        quantityText.text = string.Empty;
    }
}