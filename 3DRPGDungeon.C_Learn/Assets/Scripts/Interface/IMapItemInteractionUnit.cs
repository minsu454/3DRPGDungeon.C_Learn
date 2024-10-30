using UnityEngine;

/// <summary>
/// 맵 상호작용 가능한 유닛 인터페이스
/// </summary>
public interface IMapItemInteractionUnit
{
    /// <summary>
    /// AddForce로 방향만큼 힘 주는 함수
    /// </summary>
    public void AddImpulseForce(Vector3 dir, float power);
}