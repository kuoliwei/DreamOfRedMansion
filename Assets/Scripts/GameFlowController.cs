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

        [Header("Idle ���A�v������")]
        public IdleVideoPlayer idleVideoPlayer;

        private void Awake()
        {
            _stateMachine = new GameStateMachine();
            _stateMachine.OnStateChanged += HandleStateChanged;
        }

        private void Start()
        {
            // ��l�Ƽv������
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
                    screenUI.ShowQuestion(); // �s�W�G�������D�صe��
                    StartCoroutine(questionManager.RunQuestionFlow(() =>
                    {
                        _stateMachine.ChangeState(GameState.Result);
                    }));
                    break;
                case GameState.Result:
                    // ���A�@���� �� �ߧY��ܵ��G�e��
                    screenUI.ShowResult();

                    // �� ResultCalculator �t�d��J���⤺�e
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

            // �קK���Ƹj�w�ƥ�
            handRaiseDetector.OnHandsRaised = null;
            handRaiseDetector.OnHandsRaised += () =>
            {
                _stateMachine.ChangeState(GameState.Question);
            };
        }
    }
}
