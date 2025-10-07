using System;
using UnityEngine;

namespace DreamOfRedMansion.Core
{
    public class GameStateMachine
    {
        public GameState CurrentState { get; private set; } = GameState.Idle;

        public event Action<GameState> OnStateChanged;

        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState)
                return;

            CurrentState = newState;
            OnStateChanged?.Invoke(newState);

            Debug.Log($"[StateMachine] ª¬ºA¤Á´«¬°¡G{newState}");
        }
    }
}
