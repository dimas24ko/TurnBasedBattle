using System;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine {
    public interface IGameplayState {
        public void OnEnterState();
        public void OnExitState();

        public event Action<GameplayStateType> SwitchToNextState;
    }
}
