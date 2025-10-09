using UnityEngine;
using DreamOfRedMansion.Core;

namespace DreamOfRedMansion
{
    public class GameFlowController : MonoBehaviour
    {
        [Header("References")]
        public HandRaiseDetector handRaiseDetector;
        public QuestionManager questionManager;
        public ResultCalculator resultCalculator;
        public ScreenUIController screenUI;
        public GroundEffectController groundEffect;

        private GameStateMachine _stateMachine;

        [Header("Idle 狀態影片播放器")]
        public IdleVideoPlayer idleVideoPlayer;

        private void Awake()
        {
            _stateMachine = new GameStateMachine();
            _stateMachine.OnStateChanged += HandleStateChanged;
        }

        private void Start()
        {
            // 初始化影片播放器
            if (idleVideoPlayer != null)
                idleVideoPlayer.Initialize(_stateMachine);

            EnterIdle();
        }

        private void HandleStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Idle:
                    EnterIdle();
                    break;
                case GameState.Question:
                    screenUI.ShowQuestion(); // 新增：切換到題目畫面
                    StartCoroutine(questionManager.RunQuestionFlow(() =>
                    {
                        _stateMachine.ChangeState(GameState.Result);
                    }));
                    break;
                case GameState.Result:
                    // 狀態一切換 → 立即顯示結果畫面
                    screenUI.ShowResult();

                    // 由 ResultCalculator 負責填入角色內容
                    StartCoroutine(resultCalculator.RunResultPhase(questionManager.collectedAnswers, () =>
                    {
                        _stateMachine.ChangeState(GameState.Idle);
                    }));
                    break;
            }
        }

        private void EnterIdle()
        {
            screenUI.ShowIdleScreen();
            groundEffect.SetIdle();

            // 避免重複綁定事件
            handRaiseDetector.OnHandsRaised = null;
            handRaiseDetector.OnHandsRaised += () =>
            {
                _stateMachine.ChangeState(GameState.Question);
            };
        }
    }
}
