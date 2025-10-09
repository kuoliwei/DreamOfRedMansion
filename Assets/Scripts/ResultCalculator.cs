using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DreamOfRedMansion.Data;

namespace DreamOfRedMansion
{
    public class ResultCalculator : MonoBehaviour
    {
        [Header("���G������")]
        public ResultMapping resultMapping;

        [Header("UI ���")]
        public ScreenUIController screenUI;

        [Tooltip("���G��ܬ��")]
        public float displayDuration = 20f;

        public IEnumerator RunResultPhase(string answerPattern, System.Action onComplete)
        {
            if (string.IsNullOrEmpty(answerPattern))
            {
                Debug.LogWarning("[ResultCalculator] ���ײզX���šI");
                onComplete?.Invoke();
                yield break;
            }

            Debug.Log($"[ResultCalculator] ���쵪�ײզX�G{answerPattern}");

            var result = resultMapping.GetResult(answerPattern);
            if (result == null)
            {
                Debug.LogWarning($"[ResultCalculator] �����������G�G{answerPattern}");
                onComplete?.Invoke();
                yield break;
            }

            if (screenUI != null)
                screenUI.UpdateResultContent(result.characterName, result.description, result.resultImage);

            Debug.Log($"[ResultCalculator] ��ܨ���G{result.characterName}");

            yield return new WaitForSeconds(displayDuration);
            onComplete?.Invoke();
        }

    }
}
