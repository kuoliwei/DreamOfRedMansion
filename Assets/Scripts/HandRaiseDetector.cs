using UnityEngine;
using System;
using PoseTypes; // �ޥΧA���Ѫ����[�R�W�Ŷ�

namespace DreamOfRedMansion
{
    /// <summary>
    /// �����u�����|���v�欰�A�Ω�q Idle �i�J���ʶ��q�C
    /// �|�̾ڰ��[��ơ]PersonSkeleton�^����P�_���k��ìO�_����ӻH�C
    /// </summary>
    public class HandRaiseDetector : MonoBehaviour
    {
        [Header("�Ұʱ���]�w")]
        [Tooltip("����ݰ���ӻH���򪺬��")]
        public float requiredHoldSeconds = 1.0f;

        [Tooltip("�O�_��X�����T��")]
        public bool debugLog = true;

        /// <summary>�����������|���F�Ю�Ĳ�o�C</summary>
        public Action OnHandsRaised;

        // ���A����
        private float _raiseTimer = 0f;
        private bool _hasTriggered = false;

        [Header("���[��ƨӷ�")]
        [Tooltip("�~�� WebSocket Receiver �ΰ��[�޲z����s����ƪ���")]
        public FrameSample currentFrame; // �i�� WebSocket �ݪ��������Χ�s

        [Tooltip("�Y�䴩�h�H�A�i���w�n�l�ܲĴX�H (0=�Ĥ@��)")]
        public int personIndex = 0;

        private void Update()
        {
            if (currentFrame == null || currentFrame.persons == null || currentFrame.persons.Count == 0)
            {
                ResetState();
                return;
            }

            // ���ը��o���w�H��
            if (personIndex < 0 || personIndex >= currentFrame.persons.Count)
            {
                ResetState();
                return;
            }

            var person = currentFrame.persons[personIndex];

            // ���ը��o���n���`
            if (!person.TryGet(JointId.LeftShoulder, out var ls) ||
                !person.TryGet(JointId.RightShoulder, out var rs) ||
                !person.TryGet(JointId.LeftWrist, out var lw) ||
                !person.TryGet(JointId.RightWrist, out var rw))
            {
                ResetState();
                return;
            }

            // �T�{���ĩʡ]���� conf=0 ���I�z�Z�^
            if (!ls.IsValid || !rs.IsValid || !lw.IsValid || !rw.IsValid)
            {
                ResetState();
                return;
            }

            // ��ǪӻH���ס]�������^
            float shoulderY = (ls.y + rs.y) * 0.5f;
            bool leftUp = lw.y > shoulderY;
            bool rightUp = rw.y > shoulderY;

            if (leftUp && rightUp)
            {
                _raiseTimer += Time.deltaTime;
                if (!_hasTriggered && _raiseTimer >= requiredHoldSeconds)
                {
                    _hasTriggered = true;
                    OnHandsRaised?.Invoke();

                    if (debugLog)
                        Debug.Log($"[HandRaiseDetector] ��������|���F {_raiseTimer:F2}s�AĲ�o�Ұʨƥ�");
                }
            }
            else
            {
                ResetState();
            }
        }

        private void ResetState()
        {
            _raiseTimer = 0f;
            _hasTriggered = false;
        }

        /// <summary>
        /// �i�� WebSocket ��������s�ثe�v��C
        /// �]�Ҧp�b WebSocketMessageReceiver_Async ���I�s�^
        /// </summary>
        public void UpdateFrame(FrameSample frame)
        {
            currentFrame = frame;
        }
    }
}
