using System;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine.States {
    public class SetTargetPlayerState : IGameplayState{
        public void OnEnterState() {
            throw new NotImplementedException();
        }

        public void OnExitState() {
            throw new NotImplementedException();
        }

        public event Action<GameplayStateType> SwitchToNextState;
    }
}
