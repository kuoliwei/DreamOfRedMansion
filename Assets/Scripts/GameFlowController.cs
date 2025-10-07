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

        private void Awake()
        {
            _stateMachine = new GameStateMachine();
            _stateMachine.OnStateChanged += HandleStateChanged;
        }

        private void Start()
        {
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
                    StartCoroutine(questionManager.RunQuestionFlow(() =>
                    {
                        _stateMachine.ChangeState(GameState.Result);
                    }));
                    break;
                case GameState.Result:
                    StartCoroutine(resultCalculator.ShowResult(() =>
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
