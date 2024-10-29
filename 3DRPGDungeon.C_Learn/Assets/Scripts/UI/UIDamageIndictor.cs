using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageIndictor : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private float maxAlpha = 0.3f;
    [SerializeField] private float duration = 0.6f;

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
