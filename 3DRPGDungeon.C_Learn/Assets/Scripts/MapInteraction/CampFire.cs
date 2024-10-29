using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MapInteraction
{
    public int damage;
    public float damageRate;

    private List<IDamagable> thingList = new List<IDamagable>();

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(DealDamage), 0, damageRate);
    }

    private void DealDamage()
    {
        for (int i = 0; i < thingList.Count; i++)
        {
            thingList[i].TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagalbe))
        {
            thingList.Add(damagalbe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagalbe))
        {
            thingList.Remove(damagalbe);
        }
    }
}
