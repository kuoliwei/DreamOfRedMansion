using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamOfRedMansion.Data;

namespace DreamOfRedMansion
{
    public class QuestionManager : MonoBehaviour
    {
        [Header("題庫來源")]
        public QuestionSet questionSet;

        [Header("答題時間限制（秒）")]
        public float questionDuration = 10f;

        private List<QuestionData> _currentQuestions;
        private int _currentIndex = 0;

        public IEnumerator RunQuestionFlow(Action onComplete)
        {
            if (questionSet == null)
            {
                Debug.LogError("未指定題庫 QuestionSet！");
                yield break;
            }

            _currentQuestions = questionSet.GetRandomQuestions(4);
            _currentIndex = 0;

            Debug.Log($"[QuestionManager] 啟動答題階段，共 {_currentQuestions.Count} 題。");

            while (_currentIndex < _currentQuestions.Count)
            {
                var q = _currentQuestions[_currentIndex];
                Debug.Log($"[Question] 第{_currentIndex + 1}題：{q.questionText}");

                yield return StartCoroutine(AskQuestion(q));

                _currentIndex++;
            }

            Debug.Log("[QuestionManager] 所有題目完成。");
            onComplete?.Invoke();
        }

        private IEnumerator AskQuestion(QuestionData question)
        {
            // 顯示題目（未接UI，先印Log）
            Debug.Log($"顯示題目：{question.questionText}");

            float timer = 0f;
            while (timer < questionDuration)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            // 時間到，鎖定答案（這裡暫不實作圈叉判斷）
            Debug.Log($"[Question] 時間到，自動鎖定答案（待整合腳部偵測）");
        }
    }
}
