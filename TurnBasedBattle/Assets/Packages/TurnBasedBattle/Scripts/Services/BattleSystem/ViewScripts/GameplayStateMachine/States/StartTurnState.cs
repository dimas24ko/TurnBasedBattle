using System;
using System.Collections.Generic;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine.States
{
    public class StartTurnState : IGameplayState {

        private TurnContext _context;

        public void OnEnterState() {


        }

        public void OnExitState() {
        }

        public event Action<GameplayStateType> SwitchToNextState;
    }
}
