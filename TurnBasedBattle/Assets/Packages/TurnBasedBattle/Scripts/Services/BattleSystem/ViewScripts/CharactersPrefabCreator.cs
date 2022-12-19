using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using UnityEngine;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts {
    public class CharactersPrefabCreator : MonoBehaviour {
        private PrefabLoader _prefabLoader;
        private CharactersContainer _charactersContainer;

        public float firstLineXOffset;
        public float xLineStep;

        public float maxYCharacterPosition;
        public float minYCharacterPosition;

        [Inject]
        public void Construct(PrefabLoader prefabLoader, CharactersContainer charactersContainer) {
            _prefabLoader = prefabLoader;
            _charactersContainer = charactersContainer;
        }

        public void CreateCharacters() {
            float lineXPosition = -firstLineXOffset;

            foreach (CharactersLine line in _charactersContainer.PlayerCharactersLines) {
                foreach (var character in line.CharactersInLine) {

                }

                lineXPosition -= xLineStep;
            }
        }

        private void CreateCharacter(string ) {

        }

    }
}
