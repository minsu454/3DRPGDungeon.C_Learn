using Common.Yield;
using System;
using System.Collections;
using UnityEngine;

namespace Common.CoTimer
{
    public static class CoTimer
    {
        /// <summary>
        /// 타이머 기능
        /// </summary>
        public static IEnumerator Start(float delayTime, Action callback)
        {
            yield return YieldCache.WaitForSeconds(delayTime);
            callback();
        }
    }
}