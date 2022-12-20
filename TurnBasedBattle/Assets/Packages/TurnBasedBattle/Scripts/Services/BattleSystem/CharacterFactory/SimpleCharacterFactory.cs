using System;
using TurnBasedBattle.Scripts.Services.BattleSystem.Behaviours;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharacterFactory
{
    public class SimpleCharacterFactory : ICharacterFactory {
        private CharactersDataContainer _characterDataContainer;

        [Inject]
        public SimpleCharacterFactory(CharactersDataContainer characterDataContainer) =>
            _characterDataContainer = characterDataContainer;

        public ICharacter CreateCharacter(CharacterType characterType) {
            ICharacter newCharacter = null;
            CharacterData data = _characterDataContainer.GetDataByType(characterType);

            switch (characterType) {
                case CharacterType.Archer:
                    newCharacter = new ArcherBehaviour(data.Health, data.PrefabName,data.DamageValue, data.UltDamageValue);
                    break;
                case CharacterType.Magician:
                    break;
                case CharacterType.Doctor:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(characterType), characterType, null);
            }

            return newCharacter;
        }
    }
}
