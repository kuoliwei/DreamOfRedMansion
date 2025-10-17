using UnityEngine;
using UnityEngine.Video;
using DreamOfRedMansion.Core;
using System.IO;

namespace DreamOfRedMansion
{
    /// <summary>
    /// ���� Idle ���A�ɼ���v���]�`������� VideoPlayer �ۦ�B�z�^
    /// </summary>
    public class IdleVideoPlayer : MonoBehaviour
    {
        [Header("�v���]�w")]
        [Tooltip("Idle ���A�U���񪺼v���ſ�")]
        public VideoClip idleClip;
        public string idleDirectoryName;

        [Tooltip("�i�J Idle ���A�O�_�۰ʼ���")]
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
                    Debug.Log("�䤣��X�A���v����");
                }
            }
            else
            {
                Debug.Log("���w��Ƨ����s�b");
            }

            // ���z�A VideoPlayer �� loop ����A���ϥΪ̦b Inspector �Ŀ�
            videoPlayer.playOnAwake = false;
        }
        private void OnPrepared(VideoPlayer vp)
        {
            vp.prepareCompleted -= OnPrepared;
            vp.Play();
            Debug.Log($"[IdleVideoPlayer] �}�l����v���G{vp.url}");
        }

        /// <summary>
        /// ��l�ơG�� GameFlowController �I�s�@��
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
            //    Debug.LogWarning("[IdleVideoPlayer] Idle �v�������w�I");
            //    return;
            //}

            //if (!videoPlayer.isPlaying)
            //{
            //    //videoPlayer.clip = idleClip;
            //    videoPlayer.Play();
            //    Debug.Log("[IdleVideoPlayer] �}�l���� Idle �v��");
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
                    Debug.Log("�䤣��X�A���v����");
                }
            }
            else
            {
                Debug.Log("���w��Ƨ����s�b");
            }
        }

        private void StopVideo()
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
                Debug.Log("[IdleVideoPlayer] �����v��");
            }
        }
    }
}
