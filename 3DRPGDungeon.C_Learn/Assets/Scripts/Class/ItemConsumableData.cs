using System;

/// <summary>
/// 소모성 아이템에 데이터 class
/// </summary>
[Serializable]
public class ItemConsumableData
{
    public ConsumableType type; //소모품 능력 타입
    public float Value;         //값
    public float Duration;      //지속시간
}

