using UnityEngine;
using UnityEngine.Video;
using DreamOfRedMansion.Core;

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

        [Tooltip("�i�J Idle ���A�O�_�۰ʼ���")]
        public bool autoPlayOnIdle = true;

        [SerializeField] private VideoPlayer videoPlayer;
        private GameStateMachine stateMachine;

        private void Awake()
        {
            videoPlayer.clip = idleClip;

            // ���z�A VideoPlayer �� loop ����A���ϥΪ̦b Inspector �Ŀ�
            videoPlayer.playOnAwake = false;
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
            if (idleClip == null)
            {
                Debug.LogWarning("[IdleVideoPlayer] Idle �v�������w�I");
                return;
            }

            if (!videoPlayer.isPlaying)
            {
                videoPlayer.clip = idleClip;
                videoPlayer.Play();
                Debug.Log("[IdleVideoPlayer] �}�l���� Idle �v��");
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
