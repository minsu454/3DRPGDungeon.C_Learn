using UnityEngine;
using UnityEngine.UI;

public class UICondition : MonoBehaviour
{
    public Image uiConditionBar;

    private float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue = 0;

    public void Init()
    {
        curValue = startValue;
    }

    private void ChangeUIBar()
    {
        uiConditionBar.fillAmount = curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
        ChangeUIBar();
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
        ChangeUIBar();
    }
}
