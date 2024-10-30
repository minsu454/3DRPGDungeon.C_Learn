using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MapInteraction
{
    [SerializeField] private LayerMask layerMask;           //검사해올 레이어
    [SerializeField] private float damageRate;              //데미지 주기
    [SerializeField] private LaserData[] LaserPointArr;     //레이저데이터 리스트

    private void Start()
    {
        InvokeRepeating(nameof(DealDamage), 0, damageRate);
    }

    private void Update()
    {
        DrawLaser();
    }

    /// <summary>
    /// 레이에 라인 그려주는 함수
    /// </summary>
    private void DrawLaser()
    {
        for (int i = 0; i < LaserPointArr.Length; i++)
        {
            LaserPointArr[i].lineRenderer.SetPosition(0, LaserPointArr[i].laserTr.position);
            Vector3 targetVec = LaserPointArr[i].laserTr.position + (LaserPointArr[i].laserTr.forward.normalized * LaserPointArr[i].distance);
            LaserPointArr[i].lineRenderer.SetPosition(1, targetVec);
        }
    }

    /// <summary>
    /// 지속 딜 입히는 함수
    /// </summary>
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
