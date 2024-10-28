using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    [SerializeField] private float power;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IMapInteractionUnit unit))
        {
            unit.AddImpulseForce(Vector3.up, power);
        }
    }
}
