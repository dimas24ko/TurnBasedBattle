using System;
using System.Collections.Generic;
using TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine.States;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine {
    public class GameplayStateMachine {
        private readonly Dictionary<GameplayStateType, IGameplayState> _statesMap = new Dictionary<GameplayStateType, IGameplayState>() {
            {GameplayStateType.StartGame, new StartGameState()},
            {GameplayStateType.StartTurn, new StartTurnState()},
            {GameplayStateType.SetShootCharacter, new SetShootPlayerState()},
            {GameplayStateType.SetAction, new SetActionState()},
            {GameplayStateType.SetTargetCharacter, new SetTargetPlayerState()},
            {GameplayStateType.ApplyAction, new ApplyActionState()},
            {GameplayStateType.EndTurn, new EndTurnState()},
            {GameplayStateType.EndGame, new EndGameState()},
        };

        public event EventHandler<GameplayStateType> OnStateChanged;

        private IGameplayState _currentState;

        public void ChangeState(GameplayStateType newState) {
            _currentState.OnExitState();

            IGameplayState newStateObject = _statesMap[newState];
            newStateObject.SwitchToNextState += ChangeState;
            newStateObject.OnEnterState();
            _currentState = newStateObject;

            OnStateChanged?.Invoke(this, newState);
        }
    }
}
