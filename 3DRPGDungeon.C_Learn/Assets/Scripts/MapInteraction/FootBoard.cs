using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBoard : MonoBehaviour
{
    [SerializeField] private DirType dir;           //움직일 방향 타입
    [SerializeField] private float distance;        //거리
    [SerializeField] private float duration;        //지속시간

    private Tween doMoveTween;                      //dotween 저장 변수

    private void OnEnable()
    {
        DoMove();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.contacts[0].point.y < collision.gameObject.transform.position.y)
            {
                collision.gameObject.transform.SetParent(transform);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

    private void OnDisable()
    {
        doMoveTween.Kill();
    }

    /// <summary>
    /// 설정 값에 따라 움직이는 함수
    /// </summary>
    private void DoMove()
    {
        Vector3 endVec = transform.position + GetDiraction() * distance;

        doMoveTween = transform.DOMove(endVec, duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 방향으로 방향벡터 값 바꿔주는 함수
    /// </summary>
    private Vector3 GetDiraction()
    {
        Vector3 dirVec = Vector3.zero;

        switch (dir)
        {
            case DirType.Left:
                dirVec = -transform.right;
                break;
            case DirType.Right:
                dirVec = transform.right;
                break;
            case DirType.Forward:
                dirVec = transform.forward;
                break;
            case DirType.Back:
                dirVec = -transform.forward;
                break;
            case DirType.Up:
                dirVec = transform.up;
                break;
            case DirType.Down:
                dirVec = -transform.up;
                break;
        }

        return dirVec;
    }
}
