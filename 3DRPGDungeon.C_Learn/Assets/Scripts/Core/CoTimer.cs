using Common.Yield;
using System;
using System.Collections;
using UnityEngine;

namespace Common.CoTimer
{
    public static class CoTimer
    {
        public static IEnumerator Start(float delayTime, Action callback)
        {
            yield return YieldCache.WaitForSeconds(delayTime);
            callback();
        }
    }
}