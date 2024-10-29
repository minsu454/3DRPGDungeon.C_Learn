using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MapInteraction
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float damageRate;
    [SerializeField] private LaserData[] LaserPointArr;

    private List<IDamagable> thingList = new List<IDamagable>();

    private void Start()
    {
        InvokeRepeating(nameof(DealDamage), 0, damageRate);
    }

    private void Update()
    {
        DrawLaser();
    }

    private void DrawLaser()
    {
        for (int i = 0; i < LaserPointArr.Length; i++)
        {
            LaserPointArr[i].lineRenderer.SetPosition(0, LaserPointArr[i].laserTr.position);
            Vector3 targetVec = LaserPointArr[i].laserTr.position + (LaserPointArr[i].laserTr.forward.normalized * LaserPointArr[i].distance);
            LaserPointArr[i].lineRenderer.SetPosition(1, targetVec);
        }
    }

    private void DealDamage()
    {
        for (int i = 0; i < LaserPointArr.Length; i++)
        {
            Ray ray = new Ray(LaserPointArr[i].laserTr.position, LaserPointArr[i].laserTr.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, LaserPointArr[i].distance, layerMask);

            if (hits.Length == 0)
                continue;

            for (int j = 0; j < hits.Length; j++)
            {
                hits[j].collider.GetComponent<IDamagable>().TakeDamage(LaserPointArr[i].damage);
            }
        }
    }
}
