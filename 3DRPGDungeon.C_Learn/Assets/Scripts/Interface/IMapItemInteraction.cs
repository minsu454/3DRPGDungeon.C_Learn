/// <summary>
/// 맵아이템 상호작용 인터페이스
/// </summary>
public interface IMapItemInteraction
{
    /// <summary>
    /// 상호작용하는 아이템인지 비교하는 함수
    /// </summary>
    public bool EqualsItem(string itemName);

    /// <summary>
    /// 상호작용을 성공했을 때 실행하는 함수
    /// </summary>
    public void Select();
}
