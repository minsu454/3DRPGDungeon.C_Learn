using System;
using UnityEngine;

/// <summary>
/// 레이저데이터를 저장하는 class
/// </summary>
[Serializable]
public class LaserData
{
    public Transform laserTr;
    public LineRenderer lineRenderer;
    public float distance;
    public float damage;
}