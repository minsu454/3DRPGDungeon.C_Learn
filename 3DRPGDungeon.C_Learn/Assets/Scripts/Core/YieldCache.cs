using System.Collections.Generic;
using UnityEngine;

namespace Common.Yield
{
    public static class YieldCache
    {
        private static readonly Dictionary<float, WaitForSeconds> waitForSecondsDic = new Dictionary<float, WaitForSeconds>();  //생성한 WaitForSeconds저장 dictionary

        /// <summary>
        /// WaitForSeconds 반환해주는 함수
        /// </summary>
        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            if (!waitForSecondsDic.TryGetValue(seconds, out var waitForSeconds))
            {
                waitForSeconds = new WaitForSeconds(seconds);
                waitForSecondsDic[seconds] = waitForSeconds;
            }

            return waitForSeconds;
        }
    }
}
