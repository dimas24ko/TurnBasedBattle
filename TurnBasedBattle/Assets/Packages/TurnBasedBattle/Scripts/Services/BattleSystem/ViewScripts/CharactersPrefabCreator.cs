using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts {
    public class CharactersPrefabCreator : MonoBehaviour {
        public CharacterInfoContainer CharacterInfoContainer;

        private PrefabLoader _prefabLoader;
        private CharactersContainer _charactersContainer;

        public float firstLineXOffset;
        public float xLineStep;
        public float characterZPosition;

        public float maxYCharacterPosition;
        public float minYCharacterPosition;

        public Action OnPrefabsCreated;

        [Inject]
        public void Construct(PrefabLoader prefabLoader, CharactersContainer charactersContainer) {
            _prefabLoader = prefabLoader;
            _charactersContainer = charactersContainer;
        }

        private void Awake() =>
            Execute();

        private async void Execute() {
            await CreatePlayerCharacters();
            await CreateEnemyCharacters();
            await OnPrefabsCreate();
        }

        private Task OnPrefabsCreate() {
            OnPrefabsCreated?.Invoke();
            return Task.CompletedTask;
        }

        private async Task CreatePlayerCharacters() =>
            CharacterInfoContainer.PlayerCharacters = await CreateCharacters(_charactersContainer.PlayerCharactersLines, firstLineXOffset, xLineStep);

        private async Task CreateEnemyCharacters() =>
            CharacterInfoContainer.EnemyCharacters = await CreateCharacters(_charactersContainer.EnemyCharactersLines, -firstLineXOffset, -xLineStep);

        private async Task<List<CharacterInfo>> CreateCharacters(List<CharactersLine> lines, float firstLineOffset, float lineStep) {
            var newCharacters = new List<CharacterInfo>();
            float lineXPosition = firstLineOffset;

            foreach (CharactersLine line in lines) {
                float yStep = (Math.Abs(minYCharacterPosition) + Math.Abs(maxYCharacterPosition)) / line.CharactersInLine.Count;
                int yPositionIndex = 0;
                foreach (ICharacter character in line.CharactersInLine) {
                    CharacterInfo newCharacter = await CreateCharacter(character, new Vector3(lineXPosition, minYCharacterPosition + yStep * yPositionIndex, characterZPosition));
                    newCharacters.Add(newCharacter);
                    yPositionIndex += 1;
                }

                lineXPosition += lineStep;
            }

            return newCharacters;
        }

        private async Task<CharacterInfo> CreateCharacter(ICharacter character, Vector3 spawnPosition) {
            GameObject prefab = await _prefabLoader.LoadCharacterPrefab(character.CharacterPrefabName);
            GameObject newCharacter = Instantiate(prefab, spawnPosition, Quaternion.identity);

            CharacterInfo info = newCharacter.GetComponent<CharacterInfo>();
            info.character = character;
            character.CharacterPrefab = newCharacter;

            return info;
        }
    }
}
