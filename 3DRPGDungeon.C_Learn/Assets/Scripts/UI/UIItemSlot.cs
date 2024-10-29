using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIItemSlot : MonoBehaviour
{
    public string ItemName { get; private set; } = string.Empty;
    public BaseItemSO Item { get; private set; }

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
        if (ItemName == string.Empty)
            return true;

        if (ItemName == name)
        {
            ConsumableItemSO temp = Item as ConsumableItemSO;

            if (temp != null && Quantity < temp.maxStack)
            {
                return true;
            }
        }

        return false;
    }

    public void Add(string name)
    {
        if (ItemName == string.Empty)
        {
            ItemName = name;

            Managers.Addressable.LoadItemAsync<Sprite>(name, (sprite) =>
            {
                if (this == null)
                    return;

                if (ItemName != name)
                    return;

                icon.sprite = sprite;
                icon.gameObject.SetActive(true);
                icon.DOFade(1.0f, 0.5f).From(0);
            });
            Item = Managers.Addressable.LoadItem<BaseItemSO>(name);
        }

        Quantity++;
        quantityText.text = Quantity > 1 ? Quantity.ToString() : string.Empty;

        if (outline != null)
        {
            SetOutLine();
        }
    }

    public void Remove(out bool delete)
    {
        Quantity--;

        delete = false;

        if (Quantity == 0)
        {
            Clear();
            delete = true;
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
        ItemName = string.Empty;
        icon.sprite = null;
        quantityText.text = string.Empty;
    }
}