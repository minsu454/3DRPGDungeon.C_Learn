using Common.CoTimer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MapInteraction
{
    [SerializeField] private float jumpPower;   //점프 파워
    [SerializeField] private float duraction;   //지속시간

    private Coroutine coTimer = null;           //저장해둘 코루틴

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IMapItemInteractionUnit unit))
        {
            JumpStart(collision, unit);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IMapItemInteractionUnit unit))
        {
            if(coTimer != null)
                StopCoroutine(coTimer);
        }
    }

    /// <summary>
    /// 타이머경과 후 점프시켜주는 함수
    /// </summary>
    public void JumpStart(Collision collision, IMapItemInteractionUnit unit)
    {
        if (collision.contacts[0].point.y < collision.gameObject.transform.position.y)
        {
            coTimer = StartCoroutine(CoTimer.Start(duraction, () =>
            {
                if (this == null)
                    return;

                unit.AddImpulseForce(Vector3.up, jumpPower);
            }));
        }
    }
}
