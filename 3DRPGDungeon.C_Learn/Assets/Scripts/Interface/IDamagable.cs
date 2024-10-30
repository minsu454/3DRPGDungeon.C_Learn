/// <summary>
/// 손상시키는 것이 가능한 인터페이스
/// </summary>
public interface IDamagable
{
    /// <summary>
    /// 데미지를 줄 때 함수
    /// </summary>
    public bool TakeDamage(float damage);
}
