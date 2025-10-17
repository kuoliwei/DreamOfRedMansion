using UnityEngine;
using UnityEngine.Video;
using DreamOfRedMansion.Core;
using System.IO;

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
        public string idleDirectoryName;

        [Tooltip("進入 Idle 狀態是否自動播放")]
        public bool autoPlayOnIdle = true;

        [SerializeField] private VideoPlayer videoPlayer;
        private GameStateMachine stateMachine;

        private void Awake()
        {
            //videoPlayer.clip = idleClip;
            string videoPath = Path.Combine(Application.dataPath, idleDirectoryName);
            if (Directory.Exists(videoPath))
            {
                string[] files = Directory.GetFiles(videoPath);
                if (Path.GetExtension(files[0]) == ".mp4")
                {
                    videoPlayer.url = files[0];
                    videoPlayer.Prepare();
                    //videoPlayer.prepareCompleted += OnPrepared;
                }
                else
                {
                    Debug.Log("找不到合適的影片檔");
                }
            }
            else
            {
                Debug.Log("指定資料夾不存在");
            }

            // 不干涉 VideoPlayer 的 loop 控制，讓使用者在 Inspector 勾選
            videoPlayer.playOnAwake = false;
        }
        private void OnPrepared(VideoPlayer vp)
        {
            vp.prepareCompleted -= OnPrepared;
            vp.Play();
            Debug.Log($"[IdleVideoPlayer] 開始播放影片：{vp.url}");
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
            //if (idleClip == null)
            //{
            //    Debug.LogWarning("[IdleVideoPlayer] Idle 影片未指定！");
            //    return;
            //}

            //if (!videoPlayer.isPlaying)
            //{
            //    //videoPlayer.clip = idleClip;
            //    videoPlayer.Play();
            //    Debug.Log("[IdleVideoPlayer] 開始播放 Idle 影片");
            //}
            string videoPath = Path.Combine(Application.dataPath, idleDirectoryName);
            if (Directory.Exists(videoPath))
            {
                string[] files = Directory.GetFiles(videoPath);
                if (Path.GetExtension(files[0]) == ".mp4")
                {
                    videoPlayer.url = files[0];
                    videoPlayer.Prepare();
                    videoPlayer.prepareCompleted += OnPrepared;
                }
                else
                {
                    Debug.Log("找不到合適的影片檔");
                }
            }
            else
            {
                Debug.Log("指定資料夾不存在");
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
