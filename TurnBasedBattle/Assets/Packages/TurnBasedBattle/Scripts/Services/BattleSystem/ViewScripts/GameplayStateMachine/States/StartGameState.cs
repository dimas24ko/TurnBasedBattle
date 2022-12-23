using System;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine.States {
    public class StartGameState : IGameplayState {

        public void OnEnterState() =>
            SwitchToNextState?.Invoke(GameplayStateType.StartTurn);

        public void OnExitState() {

        }

        public event Action<GameplayStateType> SwitchToNextState;
    }
}
