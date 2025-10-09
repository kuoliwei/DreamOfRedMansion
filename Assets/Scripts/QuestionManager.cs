using System;
using System.Collections;
using UnityEngine;
using DreamOfRedMansion.Data;
using System.Collections.Generic;

namespace DreamOfRedMansion
{
    public class QuestionManager : MonoBehaviour
    {
        [Header("�D�w")]
        public QuestionSet questionSet;

        [Header("UI ���")]
        public QuestionPanelController questionPanelController;

        [Header("�C�D���D�ɶ�")]
        public float questionDuration = 10f;

        // �令�r���x�s���ס]�Ҧp "1010"�^
        [NonSerialized]
        public string collectedAnswers = "";

        private List<QuestionData> _questions;

        bool answer = false;
        public IEnumerator RunQuestionFlow(Action onComplete)
        {
            if (questionSet == null)
            {
                Debug.LogError("[QuestionManager] �����w�D�w�I");
                yield break;
            }

            _questions = questionSet.questions; // �T�w�|�D�̧�
            collectedAnswers = "";

            Debug.Log($"[QuestionManager] �Ұ��D�ض��q�A�@ {_questions.Count} �D�C");
            for (int i = 0; i < _questions.Count; i++)
            {
                yield return StartCoroutine(AskQuestion(_questions[i]));
            }

            Debug.Log($"[QuestionManager] �Ҧ��D�ا��� �� ���ײզX�G{collectedAnswers}");
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
                    Debug.Log($"[QuestionManager] �D�ءu{question.questionTitle}�v���סG{(answer ? "�O" : "�_")} (�w�O��)");
                }
                else
                {
                    Debug.Log($"[QuestionManager] �D�ءu{question.questionTitle}�v���סG{(answer ? "�O" : "�_")} (���O��)");
                }
            }
            else
            {
                Debug.Log($"[QuestionManager] ��ܹL���G�u{question.questionTitle}�v");
                yield return new WaitForSeconds(questionDuration);
            }
        }
        public void SetAnswer(bool value)
        {
            answer = value;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                answer = true;
                Debug.Log($"answer:{answer}");
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                answer = false;
                Debug.Log($"answer:{answer}");
            }
        }
    }
}
