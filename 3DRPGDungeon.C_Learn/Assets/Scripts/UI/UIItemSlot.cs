using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIItemSlot : MonoBehaviour
{
    public string ItemName { get; private set; } = string.Empty;    //아이템 이름
    public BaseItemSO Item { get; private set; }                    //아이템 SO

    [SerializeField] private Image icon;                            //아이콘
    [SerializeField] private TextMeshProUGUI quantityText;          //아이템 중첩갯수 text
    [SerializeField] private Outline outline;                       //아이템 커서가 가르킬 시 사용할 outline

    private int quantity;                                           //아이템 중첩횟수

    private bool equipped = false;                                  //아이템이 슬롯에 있는지 체크해주는 함수
    public bool Equipped
    {
        get { return equipped; }
        set
        {
            equipped = value;
            SetOutLine();
        }
    }

    /// <summary>
    /// 아이템이 들어올 수 있는지 검사해주는 함수
    /// </summary>
    public bool IsPossible(string name)
    {
        if (ItemName == string.Empty)
            return true;

        if (ItemName == name)
        {
            ConsumableItemSO temp = Item as ConsumableItemSO;

            if (temp != null && quantity < temp.maxStack)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 아이템 추가해주는 함수
    /// </summary>
    public void Add(string name)
    {
        if (ItemName == string.Empty)
        {
            ItemName = name;

            Managers.Addressable.LoadDataAsync<Sprite>(name, (sprite) =>
            {
                if (this == null)
                    return;

                if (ItemName != name)
                    return;

                icon.sprite = sprite;
                icon.gameObject.SetActive(true);
                icon.DOFade(1.0f, 0.5f).From(0);
            });
            Item = Managers.Addressable.LoadData<BaseItemSO>(name);
        }

        quantity++;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)
        {
            SetOutLine();
        }
    }

    /// <summary>
    /// 아이템 삭제하는 함수
    /// </summary>
    public void Remove(out bool delete)
    {
        quantity--;

        delete = false;

        if (quantity == 0)
        {
            Clear();
            delete = true;
            return;
        }

        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
    }

    /// <summary>
    /// OutLine설정해주는 함수
    /// </summary>
    public void SetOutLine()
    {
        outline.enabled = equipped;
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Clear()
    {
        ItemName = string.Empty;
        icon.sprite = null;
        quantityText.text = string.Empty;
    }
}