using System.Collections.Generic;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData {
    public class CharactersContainer {
        public List<CharactersLine> PlayerCharactersLines => playerCharactersLines;
        public List<CharactersLine> EnemyCharactersLines => enemyCharactersLines;

        private List<CharactersLine> playerCharactersLines;
        private List<CharactersLine> enemyCharactersLines;

        public void AddCharacterToPLayerCharacters(ICharacter character, int lineIndex) =>
            AddCharacterToLine(character, lineIndex, playerCharactersLines);

        public void AddCharacterToEnemyCharacters(ICharacter character, int lineIndex) =>
            AddCharacterToLine(character, lineIndex, enemyCharactersLines);

        public void DeleteCharacterToPLayerCharacters(ICharacter character, int lineIndex) =>
            DeleteCharacterInLine(character, lineIndex, playerCharactersLines);

        public void DeleteCharacterToEnemyCharacters(ICharacter character, int lineIndex) =>
            DeleteCharacterInLine(character, lineIndex, enemyCharactersLines);

        public List<ICharacter> GetPlayerCharactersByLineIndex(int lineIndex) =>
            GetCharactersInLine(playerCharactersLines, lineIndex);

        public List<ICharacter> GetEnemyCharactersByLineIndex(int lineIndex) =>
            GetCharactersInLine(enemyCharactersLines, lineIndex);

        private List<ICharacter> GetCharactersInLine(IReadOnlyList<CharactersLine> charactersLines, int lineIndex) =>
            charactersLines[lineIndex]?.CharactersInLine;

        private void DeleteCharacterInLine(ICharacter character, int lineIndex, IList<CharactersLine> charactersLines) {
            if (charactersLines.Count <= lineIndex) {
                for (int i = 0; i < lineIndex - charactersLines.Count; i++) {
                    charactersLines.Remove(new CharactersLine());
                }

                charactersLines[lineIndex]?.CharactersInLine.Remove(character);
            }
            else {
                charactersLines[lineIndex].CharactersInLine.Remove(character);
            }
        }

        private void AddCharacterToLine(ICharacter character, int lineIndex, IList<CharactersLine> charactersLines) {
            if (charactersLines.Count <= lineIndex) {
                for (int i = 0; i < lineIndex - charactersLines.Count; i++) {
                    charactersLines.Add(new CharactersLine());
                }

                charactersLines[lineIndex]?.CharactersInLine.Add(character);
            }
            else {
                charactersLines[lineIndex].CharactersInLine.Add(character);
            }
        }
    }
}
