using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharacterFactory;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;
using TurnBasedBattle.Scripts.Services.Common;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.Generations {
    public class CharactersGenerator : IInitializable {
        private readonly JsonDataContainer _jsonDataContainer;
        private readonly CharactersContainer _charactersContainer;
        private readonly ICharacterFactory _characterFactory;

        private Dictionary<PlayerType, List<List<CharacterType>>> _playerTypeLinesMap;

        public Action OnGeneratedEnd;

        [Inject]
        public CharactersGenerator(JsonDataContainer jsonDataContainer, CharactersContainer charactersContainer, ICharacterFactory characterFactory) {
            _jsonDataContainer = jsonDataContainer;
            _charactersContainer = charactersContainer;
            _characterFactory = characterFactory;
        }

        public void Initialize() =>
            Execute();

        private void Execute() {
            GetData();

            GeneratePlayerCharacters();
            GenerateEnemyCharacters();

            OnGeneratedEnd?.Invoke();
        }

        private void GeneratePlayerCharacters() {
            int lineIndex = 0;

            foreach (List<CharacterType> lines in _playerTypeLinesMap[PlayerType.Player]) {
                foreach (CharacterType character in lines) {
                    ICharacter newCharacter = _characterFactory.CreateCharacter(character);
                    _charactersContainer.AddCharacterToPLayerCharacters(newCharacter, lineIndex);
                }

                lineIndex++;
            }

        }

        private void GenerateEnemyCharacters() {
            int lineIndex = 0;

            foreach (List<CharacterType> lines in _playerTypeLinesMap[PlayerType.Enemy]) {
                foreach (CharacterType character in lines) {
                    ICharacter newCharacter = _characterFactory.CreateCharacter(character);
                    _charactersContainer.AddCharacterToEnemyCharacters(newCharacter, lineIndex);
                }

                lineIndex++;
            }
        }

        private void GetData() =>
            _playerTypeLinesMap = JsonConvert.DeserializeObject<Dictionary<PlayerType, List<List<CharacterType>>>>(_jsonDataContainer.CharactersLinesMap.text);
    }
}
