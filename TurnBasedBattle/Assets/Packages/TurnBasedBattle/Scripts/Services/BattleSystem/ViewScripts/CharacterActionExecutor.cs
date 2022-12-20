using System;
using System.Collections.Generic;
using TurnBasedBattle.Scripts.Services.BattleSystem.Behaviours;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.UI;
using TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.ViewData;
using UnityEngine;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts {
    public class CharacterActionExecutor : MonoBehaviour {
        public CharacterInfoContainer CharacterInfoContainer;
        public CharactersPanelsSwitcher CharactersPanelsSwitcher;
        public CharactersPrefabCreator CharactersPrefabCreator;

        public CharacterInfo FirstCharacter;
        public CharacterInfo SecondCharacter;

        private CharactersContainer _charactersContainer;
        private ActionType? _cachedType;

        private bool _isPlayerTurn;

        [Inject]
        public void Construct(CharactersContainer charactersContainer) =>
            _charactersContainer = charactersContainer;

        private void Awake() =>
            CharactersPrefabCreator.OnPrefabsCreated += NewTurn;

        public void NewTurn() {
            CharactersPanelsSwitcher.HideAllPanels();

            if (_isPlayerTurn) {
                UnSubscribeOnTurn(CharacterInfoContainer.PlayerCharacters, CharacterInfoContainer.EnemyCharacters);
            }
            else {
                UnSubscribeOnTurn(CharacterInfoContainer.EnemyCharacters, CharacterInfoContainer.PlayerCharacters);
            }

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

            _cachedType = null;
            FirstCharacter = null;
            SecondCharacter = null;
        }

        public void SetActionType(ActionType type) {
            if (FirstCharacter == null) {
                return;
            }

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

        private void UnSubscribeOnTurn(List<CharacterInfo> shootingPlayer, List<CharacterInfo> targetPlayer) {
            foreach (CharacterInfo characterInfo in shootingPlayer)
            {
                characterInfo.OnCharacterSelected -= SetFirstCharacter;
            }

            foreach (CharacterInfo characterInfo in targetPlayer)
            {
                characterInfo.OnCharacterSelected -= SetSecondCharacter;
            }
        }

        private void SubscribeOnNewTurn(List<CharacterInfo> shootingPlayer, List<CharacterInfo> targetPlayer)
        {
            foreach (CharacterInfo characterInfo in shootingPlayer)
            {
                characterInfo.OnCharacterSelected += SetFirstCharacter;
                characterInfo.IsUsedInTurn = false;
            }

            foreach (CharacterInfo characterInfo in targetPlayer)
            {
                characterInfo.OnCharacterSelected += SetSecondCharacter;
                characterInfo.IsUsedInTurn = false;
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
            if (FirstCharacter == null || FirstCharacter.IsUsedInTurn) {
                return;
            }

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

        private void SetSideCharactersUnSelectable(List<CharacterInfo> sideCharacters) {
            foreach (CharacterInfo characterInfo in sideCharacters) {
                characterInfo.IsSelectable = false;
            }
        }
    }

    public enum ActionType {
        Shoot,
        Ult,
    }
}
