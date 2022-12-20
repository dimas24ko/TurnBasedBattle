using System;
using TurnBasedBattle.Scripts.Services.BattleSystem;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;
using UnityEngine;
using Zenject;

namespace TurnBasedBattle.Scripts.Services {
    public class GameLifeCycle {
        private CharactersContainer _charactersContainer;
        private CharactersGenerator _charactersGenerator;

        public Action Win;
        public Action Lose;

        [Inject]
        public GameLifeCycle(CharactersContainer charactersContainer, CharactersGenerator charactersGenerator) {
            _charactersContainer = charactersContainer;
            _charactersGenerator = charactersGenerator;

            _charactersGenerator.OnGeneratedEnd += SubscribeOnCharacters;
        }

        private void SubscribeOnCharacters() {
            foreach (CharactersLine line in _charactersContainer.EnemyCharactersLines) {
                foreach (ICharacter character in line.CharactersInLine) {
                    character.OnDied += ()=> CheckWin(character);
                }
            }

            foreach (CharactersLine line in _charactersContainer.PlayerCharactersLines) {
                foreach (ICharacter character in line.CharactersInLine) {
                    character.OnDied += ()=> CheckLose(character);
                }
            }
        }

        private void CheckWin(ICharacter character) {
            _charactersContainer.DeleteEnemyCharacter(character);

            foreach (CharactersLine line in _charactersContainer.EnemyCharactersLines) {
                if (line.CharactersInLine.Count != 0) {
                    return;
                }

                Win?.Invoke();
            }
        }

        private void CheckLose(ICharacter character) {
            _charactersContainer.DeletePLayerCharacter(character);

            foreach (CharactersLine line in _charactersContainer.EnemyCharactersLines) {
                if (line.CharactersInLine.Count != 0) {
                    return;
                }

                Lose?.Invoke();
            }
        }
    }
}
