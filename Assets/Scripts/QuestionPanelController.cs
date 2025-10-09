using UnityEngine;
using UnityEngine.UI;
using DreamOfRedMansion.Data;

namespace DreamOfRedMansion
{
    /// <summary>
    /// �t�d�����D�صe���W�Ҧ���r��ܡC
    /// �� QuestionManager �I�s�H��s�e�����e�C
    /// </summary>
    public class QuestionPanelController : MonoBehaviour
    {
        [Header("UI Text ����")]
        [Tooltip("��ܱ��Ҵy�z����r�϶�")]
        public Text contextText;

        [Tooltip("����D�إ�������r�϶�")]
        public Text questionText;

        [Tooltip("��ܪ֩w���ת���r�϶� (�O / ��)")]
        public Text positiveAnswerText;

        [Tooltip("��ܧ_�w���ת���r�϶� (�_ / �e)")]
        public Text negativeAnswerText;

        [Header("�ʵe�ίS�ġ]�i��^")]
        [Tooltip("�O�_�ҥΤ�r�H�J�ĪG")]
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
        /// ��ܫ��w�D�ؤ��e�C
        /// </summary>
        public void ShowQuestion(QuestionData question)
        {
            Clear();
            if (question == null)
            {
                Debug.LogWarning("[QuestionPanelController] �ǤJ�� QuestionData ���šI");
                return;
            }

            if (contextText != null)
                contextText.text = question.contextText;

            if (!question.isCutcene)
            {
                if (questionText != null)
                    questionText.text = question.questionText;

                if (positiveAnswerText != null)
                    positiveAnswerText.text = $"{question.optionCircle}�G{question.circleDescription}";

                if (negativeAnswerText != null)
                    negativeAnswerText.text = $"{question.optionCross}�G{question.crossDescription}";
            }

            if (useFadeIn)
                StartCoroutine(FadeIn());
        }

        /// <summary>
        /// �M����r���e�]�Ҧp�������A�ɡ^
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
                t += Time.deltaTime * 2f; // �H�J�t��
                canvasGroup.alpha = Mathf.Lerp(0, 1, t);
                yield return null;
            }
            canvasGroup.alpha = 1;
        }
    }
}
