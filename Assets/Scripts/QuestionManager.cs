using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamOfRedMansion.Data;

namespace DreamOfRedMansion
{
    public class QuestionManager : MonoBehaviour
    {
        [Header("�D�w�ӷ�")]
        public QuestionSet questionSet;

        [Header("���D�ɶ�����]��^")]
        public float questionDuration = 10f;

        private List<QuestionData> _currentQuestions;
        private int _currentIndex = 0;

        public IEnumerator RunQuestionFlow(Action onComplete)
        {
            if (questionSet == null)
            {
                Debug.LogError("�����w�D�w QuestionSet�I");
                yield break;
            }

            _currentQuestions = questionSet.GetRandomQuestions(4);
            _currentIndex = 0;

            Debug.Log($"[QuestionManager] �Ұʵ��D���q�A�@ {_currentQuestions.Count} �D�C");

            while (_currentIndex < _currentQuestions.Count)
            {
                var q = _currentQuestions[_currentIndex];
                Debug.Log($"[Question] ��{_currentIndex + 1}�D�G{q.questionText}");

                yield return StartCoroutine(AskQuestion(q));

                _currentIndex++;
            }

            Debug.Log("[QuestionManager] �Ҧ��D�ا����C");
            onComplete?.Invoke();
        }

        private IEnumerator AskQuestion(QuestionData question)
        {
            // ����D�ء]����UI�A���LLog�^
            Debug.Log($"����D�ءG{question.questionText}");

            float timer = 0f;
            while (timer < questionDuration)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            // �ɶ���A��w���ס]�o�̼Ȥ���@��e�P�_�^
            Debug.Log($"[Question] �ɶ���A�۰���w���ס]�ݾ�X�}�������^");
        }
    }
}
