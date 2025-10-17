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
        public NoseAnswerDetector noseAnswerDetector;
        public GroundEffectController groundEffectController;

        private GameStateMachine _stateMachine;

        private void Awake()
        {
            // �إߨê�l�ƪ��A��
            _stateMachine = new GameStateMachine();

            // ��ť���A�ܤ�
            _stateMachine.OnStateChanged += HandleStateChanged;

            // �N���A�༽��������
            _stateMachine.OnStateChanged += (newState) =>
            {
                handRaiseDetector?.OnGameStateChanged(newState);
                noseAnswerDetector?.OnGameStateChanged(newState);
                groundEffectController?.OnGameStateChanged(newState);
            };

            // ���� Bootstrap �� Start()�A�T�O�Ҧ��ޥΡ]�Ҧp idleVideoPlayer�^���w��l��
        }

        private void Start()
        {
            // �Ҧ��ƥ�q�\������~�s����l���A
            _stateMachine.Bootstrap();
        }

        /// <summary>
        /// ���A�ܧ�ɰ�������y�{�C
        /// </summary>
        private void HandleStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Idle:
                    EnterIdle();
                    break;

                case GameState.Question:
                    screenUI.ShowQuestion();
                    StartCoroutine(questionManager.RunQuestionFlow(() =>
                    {
                        _stateMachine.ChangeState(GameState.Result);
                    }));
                    break;

                case GameState.Result:
                    screenUI.ShowResult();
                    StartCoroutine(resultCalculator.RunResultPhase(questionManager.collectedAnswers, () =>
                    {
                        _stateMachine.ChangeState(GameState.Idle);
                    }));
                    break;
            }
        }

        /// <summary>
        /// �i�J Idle ���A�C
        /// </summary>
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
