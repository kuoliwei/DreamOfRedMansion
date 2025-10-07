using System;
using System.Collections;
using UnityEngine;

namespace DreamOfRedMansion
{
    /// <summary>
    /// 根據四題答案結果顯示角色與說明。
    /// 暫時版本：僅顯示文字 Log，延遲後回到 Idle。
    /// </summary>
    public class ResultCalculator : MonoBehaviour
    {
        [Header("顯示時間（秒）")]
        public float displayDuration = 20f;

        public IEnumerator ShowResult(Action onComplete)
        {
            Debug.Log("[ResultCalculator] 開始顯示結果（暫時以文字替代）");
            yield return new WaitForSeconds(displayDuration);
            Debug.Log("[ResultCalculator] 結果階段結束 → 回 Idle");
            onComplete?.Invoke();
        }
    }
}
