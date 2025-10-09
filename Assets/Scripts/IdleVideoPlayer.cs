using UnityEngine;
using UnityEngine.Video;
using DreamOfRedMansion.Core;

namespace DreamOfRedMansion
{
    /// <summary>
    /// 控制 Idle 狀態時播放影片（循環播放由 VideoPlayer 自行處理）
    /// </summary>
    public class IdleVideoPlayer : MonoBehaviour
    {
        [Header("影片設定")]
        [Tooltip("Idle 狀態下播放的影片剪輯")]
        public VideoClip idleClip;

        [Tooltip("進入 Idle 狀態是否自動播放")]
        public bool autoPlayOnIdle = true;

        [SerializeField] private VideoPlayer videoPlayer;
        private GameStateMachine stateMachine;

        private void Awake()
        {
            videoPlayer.clip = idleClip;

            // 不干涉 VideoPlayer 的 loop 控制，讓使用者在 Inspector 勾選
            videoPlayer.playOnAwake = false;
        }

        /// <summary>
        /// 初始化：由 GameFlowController 呼叫一次
        /// </summary>
        public void Initialize(GameStateMachine gsm)
        {
            stateMachine = gsm;
            stateMachine.OnStateChanged += HandleStateChanged;
        }

        private void OnDestroy()
        {
            if (stateMachine != null)
                stateMachine.OnStateChanged -= HandleStateChanged;
        }

        private void HandleStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Idle:
                    if (autoPlayOnIdle)
                        PlayIdle();
                    break;

                case GameState.Question:
                case GameState.Result:
                    StopVideo();
                    break;
            }
        }

        private void PlayIdle()
        {
            if (idleClip == null)
            {
                Debug.LogWarning("[IdleVideoPlayer] Idle 影片未指定！");
                return;
            }

            if (!videoPlayer.isPlaying)
            {
                videoPlayer.clip = idleClip;
                videoPlayer.Play();
                Debug.Log("[IdleVideoPlayer] 開始播放 Idle 影片");
            }
        }

        private void StopVideo()
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
                Debug.Log("[IdleVideoPlayer] 停止播放影片");
            }
        }
    }
}
