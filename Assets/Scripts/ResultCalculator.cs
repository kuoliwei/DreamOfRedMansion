using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DreamOfRedMansion.Data;

namespace DreamOfRedMansion
{
    public class ResultCalculator : MonoBehaviour
    {
        [Header("結果對應表")]
        public ResultMapping resultMapping;

        [Header("UI 控制器")]
        public ScreenUIController screenUI;

        [Tooltip("結果顯示秒數")]
        public float displayDuration = 20f;

        public IEnumerator RunResultPhase(string answerPattern, System.Action onComplete)
        {
            if (string.IsNullOrEmpty(answerPattern))
            {
                Debug.LogWarning("[ResultCalculator] 答案組合為空！");
                onComplete?.Invoke();
                yield break;
            }

            Debug.Log($"[ResultCalculator] 收到答案組合：{answerPattern}");

            var result = resultMapping.GetResult(answerPattern);
            if (result == null)
            {
                Debug.LogWarning($"[ResultCalculator] 未找到對應結果：{answerPattern}");
                onComplete?.Invoke();
                yield break;
            }

            if (screenUI != null)
                screenUI.UpdateResultContent(result.characterName, result.description, result.resultImage);

            Debug.Log($"[ResultCalculator] 顯示角色：{result.characterName}");

            yield return new WaitForSeconds(displayDuration);
            onComplete?.Invoke();
        }

    }
}
