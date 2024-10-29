using Common.CoTimer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MapInteraction
{
    [SerializeField] private float power;
    [SerializeField] private float time;

    private Coroutine coTimer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IMapInteractionUnit unit))
        {
            coTimer = StartCoroutine(CoTimer.Start(time, () =>
            {
                if (this == null)
                    return;

                unit.AddImpulseForce(Vector3.up, power);
            }));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IMapInteractionUnit unit))
        {
            StopCoroutine(coTimer);
        }
    }
}
