using Common.Yield;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MapInteraction
{
    [SerializeField] private int damage;        //데미지
    [SerializeField] private float damageRate;  //데미지 호출 주기

    private Coroutine coTakeDamage;             //코루틴 저장 변수

    private List<IDamagable> thingList = new List<IDamagable>();    //데미지 줄 친구들 저장 리스트

    /// <summary>
    /// 일정주기로 데미지 주는 코드
    /// </summary>
    private IEnumerator CoTakeDamage()
    {
        while (true)
        {
            yield return YieldCache.WaitForSeconds(damageRate);

            for (int i = 0; i < thingList.Count; i++)
            {
                thingList[i].TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagalbe))
        {
            thingList.Add(damagalbe);
            if(coTakeDamage == null)
                coTakeDamage = StartCoroutine(CoTakeDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagalbe))
        {
            thingList.Remove(damagalbe);

            if (thingList.Count == 0)
            {
                StopCoroutine(coTakeDamage);
                coTakeDamage = null;
            }
        }
    }
}
