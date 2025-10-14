using System;
using System.Collections;
using UnityEngine;
using DreamOfRedMansion.Data;
using System.Collections.Generic;

namespace DreamOfRedMansion
{
    public class QuestionManager : MonoBehaviour
    {
        [Header("題庫")]
        public QuestionSet questionSet;

        [Header("UI 控制器")]
        public QuestionPanelController questionPanelController;
        public GroundEffectController groundEffectController;

        [Header("每題答題時間")]
        public float questionDuration = 10f;

        // 改成字串儲存答案（例如 "1010"）
        [NonSerialized]
        public string collectedAnswers = "";

        private List<QuestionData> _questions;

        bool answer = false;
        public IEnumerator RunQuestionFlow(Action onComplete)
        {
            if (questionSet == null)
            {
                Debug.LogError("[QuestionManager] 未指定題庫！");
                yield break;
            }

            _questions = questionSet.questions; // 固定四題依序
            collectedAnswers = "";

            Debug.Log($"[QuestionManager] 啟動題目階段，共 {_questions.Count} 題。");
            for (int i = 0; i < _questions.Count; i++)
            {
                yield return StartCoroutine(AskQuestion(_questions[i]));
            }

            Debug.Log($"[QuestionManager] 所有題目完成 → 答案組合：{collectedAnswers}");
            onComplete?.Invoke();
        }

        private IEnumerator AskQuestion(QuestionData question)
        {
            questionPanelController.ShowQuestion(question);
            if (!question.isCutcene)
            {
                answer = false;
                yield return new WaitForSeconds(questionDuration);

                if (question.isRecord)
                {
                    collectedAnswers += answer ? "1" : "0";
                    Debug.Log($"[QuestionManager] 題目「{question.questionTitle}」答案：{(answer ? "是" : "否")} (已記錄)");
                }
                else
                {
                    Debug.Log($"[QuestionManager] 題目「{question.questionTitle}」答案：{(answer ? "是" : "否")} (未記錄)");
                }
            }
            else
            {
                Debug.Log($"[QuestionManager] 顯示過場：「{question.questionTitle}」");
                yield return new WaitForSeconds(questionDuration);
            }
        }
        public void SetAnswer(bool value)
        {
            if (value)
            {
                groundEffectController.selectCircle();
            }
            else
            {
                groundEffectController.selectCross();
            }
            answer = value;
            //Debug.Log($"answer：{answer}");
        }
        private void Update()
        {

        }
    }
}
