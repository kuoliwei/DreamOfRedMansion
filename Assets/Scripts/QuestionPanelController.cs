using UnityEngine;
using UnityEngine.UI;
using DreamOfRedMansion.Data;

namespace DreamOfRedMansion
{
    /// <summary>
    /// 負責控制題目畫面上所有文字顯示。
    /// 由 QuestionManager 呼叫以更新畫面內容。
    /// </summary>
    public class QuestionPanelController : MonoBehaviour
    {
        [Header("UI Text 元件")]
        [Tooltip("顯示情境描述的文字區塊")]
        public Text contextText;

        [Tooltip("顯示題目本身的文字區塊")]
        public Text questionText;

        [Tooltip("顯示肯定答案的文字區塊 (是 / 圈)")]
        public Text positiveAnswerText;

        [Tooltip("顯示否定答案的文字區塊 (否 / 叉)")]
        public Text negativeAnswerText;

        [Header("動畫或特效（可選）")]
        [Tooltip("是否啟用文字淡入效果")]
        public bool useFadeIn = false;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        /// <summary>
        /// 顯示指定題目內容。
        /// </summary>
        public void ShowQuestion(QuestionData question)
        {
            Clear();
            if (question == null)
            {
                Debug.LogWarning("[QuestionPanelController] 傳入的 QuestionData 為空！");
                return;
            }

            if (contextText != null)
                contextText.text = question.contextText;

            if (!question.isCutcene)
            {
                if (questionText != null)
                    questionText.text = question.questionText;

                if (positiveAnswerText != null)
                    positiveAnswerText.text = $"{question.optionCircle}：{question.circleDescription}";

                if (negativeAnswerText != null)
                    negativeAnswerText.text = $"{question.optionCross}：{question.crossDescription}";
            }

            if (useFadeIn)
                StartCoroutine(FadeIn());
        }

        /// <summary>
        /// 清除文字內容（例如切換狀態時）
        /// </summary>
        public void Clear()
        {
            if (contextText != null) contextText.text = "";
            if (questionText != null) questionText.text = "";
            if (positiveAnswerText != null) positiveAnswerText.text = "";
            if (negativeAnswerText != null) negativeAnswerText.text = "";
        }

        private System.Collections.IEnumerator FadeIn()
        {
            canvasGroup.alpha = 0;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 2f; // 淡入速度
                canvasGroup.alpha = Mathf.Lerp(0, 1, t);
                yield return null;
            }
            canvasGroup.alpha = 1;
        }
    }
}
