using UnityEngine;
using UnityEngine.UI;

public class UICondition : MonoBehaviour
{
    public Image uiConditionBar;

    public float CurValue { get; private set; }
    public float MaxValue;
    public float StartValue;
    public float PassiveValue = 0;

    public void Init()
    {
        CurValue = StartValue;
        ChangeUIBar();
    }

    private void ChangeUIBar()
    {
        uiConditionBar.fillAmount = CurValue / MaxValue;
    }

    public void Add(float value)
    {
        CurValue = Mathf.Min(CurValue + value, MaxValue);
        ChangeUIBar();
    }

    public void Subtract(float value)
    {
        CurValue = Mathf.Max(CurValue - value, 0);
        ChangeUIBar();
    }
}
