using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageIndictor : MonoBehaviour
{
    [SerializeField] private Image image;           //이미지

    [SerializeField] private float maxAlpha = 0.3f; //최대 알파값
    [SerializeField] private float duration = 0.6f; //지속시간

    /// <summary>
    /// 화면이 빨간색으로 지속시간만큼 변하는 함수
    /// </summary>
    public void Flash()
    {
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        float halfDuration = duration / 2;

        image.DOFade(maxAlpha, halfDuration).SetEase(Ease.InQuint).From(0).OnComplete(() =>
        {
            if (!gameObject.activeInHierarchy)
                return;

            image.DOFade(0, halfDuration).SetEase(Ease.OutQuint);
        });
    }
}
