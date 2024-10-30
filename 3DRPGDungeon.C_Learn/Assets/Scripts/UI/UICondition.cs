using UnityEngine;
using UnityEngine.UI;

public class UICondition : MonoBehaviour
{
    public Image uiConditionBar;                //상태바

    public float CurValue { get; private set; } //현재 값
    public float MaxValue;                      //최대 값
    public float StartValue;                    //스타트 값
    public float PassiveValue = 0;              //지속 값

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        CurValue = StartValue;
        ChangeUIBar();
    }

    /// <summary>
    /// UIBar 바꿔주는 함수
    /// </summary>
    private void ChangeUIBar()
    {
        uiConditionBar.fillAmount = CurValue / MaxValue;
    }

    /// <summary>
    /// 값이 더해지는 함수
    /// </summary>
    public void Add(float value)
    {
        CurValue = Mathf.Min(CurValue + value, MaxValue);
        ChangeUIBar();
    }

    /// <summary>
    /// 값이 빼지는 함수
    /// </summary>
    public void Subtract(float value)
    {
        CurValue = Mathf.Max(CurValue - value, 0);
        ChangeUIBar();
    }
}
