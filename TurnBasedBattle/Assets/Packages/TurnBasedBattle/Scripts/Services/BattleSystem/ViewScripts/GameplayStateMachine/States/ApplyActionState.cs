using System;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine.States
{
    public class ApplyActionState : IGameplayState {
        public void OnEnterState() {
            throw new System.NotImplementedException();
        }

        public void OnExitState() {
            throw new System.NotImplementedException();
        }

        public event Action<GameplayStateType> SwitchToNextState;
    }
}
