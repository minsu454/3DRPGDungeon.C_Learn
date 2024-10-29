using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBoard : MonoBehaviour
{
    [SerializeField] private DirType dir;
    [SerializeField] private float distance;
    [SerializeField] private float duration;

    private void Start()
    {
        DoMove();
    }

    private void DoMove()
    {
        Vector3 endVec = transform.position + GetDiraction() * distance;

        transform.DOMove(endVec, duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.contacts[0].point.y < collision.gameObject.transform.position.y)
            {
                Debug.Log("true");
                collision.gameObject.transform.SetParent(this.transform);
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
}
