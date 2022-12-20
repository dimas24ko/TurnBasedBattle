using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TurnBasedBattle.Scripts.Services.BattleSystem.Behaviours;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts;
using UnityEngine;
using Zenject;
using CharacterInfo = TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.CharacterInfo;

namespace TurnBasedBattle.Scripts.Services {
    public class CharacterActionExecutor : MonoBehaviour {
        public CharacterInfoContainer CharacterInfoContainer;
        public CharactersPanelsSwitcher CharactersPanelsSwitcher;
        public CharactersPrefabCreator CharactersPrefabCreator;

        public CharacterInfo FirstCharacter;
        public CharacterInfo SecondCharacter;

        private CharactersContainer _charactersContainer;

        private bool _isPlayerTurn;

        [Inject]
        public void Construct(CharactersContainer charactersContainer) =>
            _charactersContainer = charactersContainer;

        private void Awake() =>
            CharactersPrefabCreator.OnPrefabsCreated += NewTurn;

        public void NewTurn() {
            _isPlayerTurn = !_isPlayerTurn;

            if (_isPlayerTurn) {
                SubscribeOnNewTurn(CharacterInfoContainer.PlayerCharacters, CharacterInfoContainer.EnemyCharacters);
            }
            else {
                SubscribeOnNewTurn(CharacterInfoContainer.EnemyCharacters, CharacterInfoContainer.PlayerCharacters);
            }

            SetSideCharactersSelectable(_isPlayerTurn
                ? CharacterInfoContainer.PlayerCharacters
                : CharacterInfoContainer.EnemyCharacters);
        }

        public void SetActionType(ActionType type) {
            switch (type) {
                case ActionType.Shoot:
                    SetupSimpleShoot();
                    break;
                case ActionType.Ult:
                    ExecuteUlt(_isPlayerTurn ? _charactersContainer.EnemyCharactersLines : _charactersContainer.PlayerCharactersLines);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void SubscribeOnNewTurn(List<CharacterInfo> shootingPlayer, List<CharacterInfo> targetPlayer)
        {
            foreach (CharacterInfo characterInfo in shootingPlayer)
            {
                characterInfo.OnCharacterSelected += SetFirstCharacter;
            }

            foreach (CharacterInfo characterInfo in targetPlayer)
            {
                characterInfo.OnCharacterSelected += SetSecondCharacter;
            }
        }

        private void SetFirstCharacter(CharacterInfo characterInfo) { //TODO redev on stateMachine, but i dont khow how do it
            if (characterInfo.IsUsedInTurn) {
                return;
            }

            FirstCharacter = characterInfo;

            CharactersPanelsSwitcher.ShowPanel(FirstCharacter.character.Type, FirstCharacter.IsUltReady);
        }

        private void SetSecondCharacter(CharacterInfo characterInfo) {//TODO redev on stateMachine, but i dont khow how do it
            SecondCharacter = characterInfo;

            ExecuteSimpleShoot();
        }

        private void ExecuteSimpleShoot() {
            switch (FirstCharacter.character.Type) {
                case CharacterType.Archer:
                    var character = (ArcherBehaviour)FirstCharacter.character;
                    character.ShootTo(SecondCharacter.character);
                    break;
                case CharacterType.Magician:
                    break;
                case CharacterType.Doctor:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            FirstCharacter.IsUsedInTurn = true;

            SetSideCharactersSelectable(_isPlayerTurn
                ? CharacterInfoContainer.PlayerCharacters
                : CharacterInfoContainer.EnemyCharacters);

            CharactersPanelsSwitcher.HidePanel(FirstCharacter.character.Type);
        }

        private void SetupSimpleShoot() =>
            SetSideCharactersSelectable(_isPlayerTurn
                ? CharacterInfoContainer.EnemyCharacters
                : CharacterInfoContainer.PlayerCharacters);

        private void ExecuteUlt(List<CharactersLine> ultTargets) {
            switch (FirstCharacter.character.Type) {
                case CharacterType.Archer:
                    var character = (ArcherBehaviour)FirstCharacter.character;
                    character.UseUlt(ultTargets);
                    break;
                case CharacterType.Magician:
                    break;
                case CharacterType.Doctor:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            FirstCharacter.IsUsedInTurn = true;
            FirstCharacter.SetUltFalse();

            SetSideCharactersSelectable(_isPlayerTurn
                ? CharacterInfoContainer.PlayerCharacters
                : CharacterInfoContainer.EnemyCharacters);

            CharactersPanelsSwitcher.HidePanel(FirstCharacter.character.Type);
        }

        private void SetSideCharactersSelectable(List<CharacterInfo> sideCharacters) {
            foreach (CharacterInfo characterInfo in sideCharacters) {
                characterInfo.IsSelectable = true;
            }
        }
    }

    public enum ActionType {
        Shoot,
        Ult,
    }
}
