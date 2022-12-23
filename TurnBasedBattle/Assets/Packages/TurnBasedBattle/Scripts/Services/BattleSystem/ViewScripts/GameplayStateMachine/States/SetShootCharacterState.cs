using System;
using System.Collections.Generic;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine.States {
    public class SetShootPlayerState : IGameplayState{

        public event Action<GameplayStateType> SwitchToNextState;

        public void OnEnterState() {
            if (TurnContext.Instance.IsPlayerTurn) {
                SubscribeOnThisTurn(TurnContext.Instance.CharacterInfoContainer.PlayerCharacters);
            }
            else {
                SubscribeOnThisTurn(TurnContext.Instance.CharacterInfoContainer.EnemyCharacters);
            }
        }

        private void SubscribeOnThisTurn(List<CharacterInfo> shootingPlayer)
        {
            foreach (CharacterInfo characterInfo in shootingPlayer) {
                characterInfo.OnCharacterSelected += SetFirstCharacter;;
                characterInfo.IsUsedInTurn = false;
            }
        }

        private void UnSubscribeOnThisTurn(List<CharacterInfo> shootingPlayer)
        {
            foreach (CharacterInfo characterInfo in shootingPlayer) {
                characterInfo.OnCharacterSelected -= SetFirstCharacter;;
                characterInfo.IsUsedInTurn = false;
            }
        }

        private void SetFirstCharacter(CharacterInfo characterInfo) {
            TurnContext.Instance.FirstCharacter = characterInfo;
            SwitchToNextState?.Invoke(GameplayStateType.SetAction);
        }

        public void OnExitState() {
            if (TurnContext.Instance.IsPlayerTurn) {
                UnSubscribeOnThisTurn(TurnContext.Instance.CharacterInfoContainer.PlayerCharacters);
            }
            else {
                UnSubscribeOnThisTurn(TurnContext.Instance.CharacterInfoContainer.EnemyCharacters);
            }
        }
    }
}
