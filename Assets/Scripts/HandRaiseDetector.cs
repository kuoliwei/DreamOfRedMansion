using UnityEngine;
using System;
using PoseTypes; // 引用你提供的骨架命名空間

namespace DreamOfRedMansion
{
    /// <summary>
    /// 偵測「雙手舉高」行為，用於從 Idle 進入互動階段。
    /// 會依據骨架資料（PersonSkeleton）持續判斷左右手腕是否高於肩膀。
    /// </summary>
    public class HandRaiseDetector : MonoBehaviour
    {
        [Header("啟動條件設定")]
        [Tooltip("雙手需高於肩膀持續的秒數")]
        public float requiredHoldSeconds = 1.0f;

        [Tooltip("是否輸出除錯訊息")]
        public bool debugLog = true;

        /// <summary>當偵測到雙手舉高達標時觸發。</summary>
        public Action OnHandsRaised;

        // 狀態控制
        private float _raiseTimer = 0f;
        private bool _hasTriggered = false;

        [Header("骨架資料來源")]
        [Tooltip("外部 WebSocket Receiver 或骨架管理器更新的資料物件")]
        public FrameSample currentFrame; // 可由 WebSocket 端直接指派或更新

        [Tooltip("若支援多人，可指定要追蹤第幾人 (0=第一位)")]
        public int personIndex = 0;

        private void Update()
        {
            if (currentFrame == null || currentFrame.persons == null || currentFrame.persons.Count == 0)
            {
                ResetState();
                return;
            }

            // 嘗試取得指定人物
            if (personIndex < 0 || personIndex >= currentFrame.persons.Count)
            {
                ResetState();
                return;
            }

            var person = currentFrame.persons[personIndex];

            // 嘗試取得必要關節
            if (!person.TryGet(JointId.LeftShoulder, out var ls) ||
                !person.TryGet(JointId.RightShoulder, out var rs) ||
                !person.TryGet(JointId.LeftWrist, out var lw) ||
                !person.TryGet(JointId.RightWrist, out var rw))
            {
                ResetState();
                return;
            }

            // 確認有效性（防止 conf=0 的點干擾）
            if (!ls.IsValid || !rs.IsValid || !lw.IsValid || !rw.IsValid)
            {
                ResetState();
                return;
            }

            // 基準肩膀高度（取平均）
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
                        Debug.Log($"[HandRaiseDetector] 雙手持續舉高達 {_raiseTimer:F2}s，觸發啟動事件");
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
        /// 可由 WebSocket 接收器更新目前影格。
        /// （例如在 WebSocketMessageReceiver_Async 中呼叫）
        /// </summary>
        public void UpdateFrame(FrameSample frame)
        {
            currentFrame = frame;
        }
    }
}
