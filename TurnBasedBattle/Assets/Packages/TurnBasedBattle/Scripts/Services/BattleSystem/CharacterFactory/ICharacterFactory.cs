using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharacterFactory {
    public interface ICharacterFactory {
        public ICharacter CreateCharacter(CharacterType characterType);
    }
}
