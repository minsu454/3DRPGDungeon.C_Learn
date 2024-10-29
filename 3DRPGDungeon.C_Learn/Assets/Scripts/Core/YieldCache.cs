using System.Collections.Generic;
using UnityEngine;

namespace Common.Yield
{
    public static class YieldCache
    {
        private static readonly Dictionary<float, WaitForSeconds> waitForSecondsDic = new Dictionary<float, WaitForSeconds>();

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
