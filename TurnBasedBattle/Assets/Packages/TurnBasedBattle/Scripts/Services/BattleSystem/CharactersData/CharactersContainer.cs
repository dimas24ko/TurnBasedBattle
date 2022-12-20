using System.Collections.Generic;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData {
    public class CharactersContainer {
        public List<CharactersLine> PlayerCharactersLines => playerCharactersLines;
        public List<CharactersLine> EnemyCharactersLines => enemyCharactersLines;

        private List<CharactersLine> playerCharactersLines = new List<CharactersLine>();
        private List<CharactersLine> enemyCharactersLines = new List<CharactersLine>();

        public void AddCharacterToPLayerCharacters(ICharacter character, int lineIndex) =>
            AddCharacterToLine(character, lineIndex, playerCharactersLines);

        public void AddCharacterToEnemyCharacters(ICharacter character, int lineIndex) =>
            AddCharacterToLine(character, lineIndex, enemyCharactersLines);

        public void DeletePLayerCharacter(ICharacter character) =>
            DeleteCharacter(character, playerCharactersLines);

        public void DeleteEnemyCharacter(ICharacter character) =>
            DeleteCharacter(character, enemyCharactersLines);

        public List<ICharacter> GetPlayerCharactersByLineIndex(int lineIndex) =>
            GetCharactersInLine(playerCharactersLines, lineIndex);

        public List<ICharacter> GetEnemyCharactersByLineIndex(int lineIndex) =>
            GetCharactersInLine(enemyCharactersLines, lineIndex);

        private List<ICharacter> GetCharactersInLine(IReadOnlyList<CharactersLine> charactersLines, int lineIndex) =>
            charactersLines[lineIndex]?.CharactersInLine;

        private void DeleteCharacter(ICharacter character, List<CharactersLine> charactersLines) {
            foreach (CharactersLine line in charactersLines) {
                foreach (ICharacter characterInLine in line.CharactersInLine) {
                    if (characterInLine == character) {
                        line.CharactersInLine.Remove(characterInLine);
                        return;
                    }
                }
            }
        }

        private void AddCharacterToLine(ICharacter character, int lineIndex, IList<CharactersLine> charactersLines) {
            if (charactersLines.Count <= lineIndex) {
                for (int i = 0; i < lineIndex - charactersLines.Count + 1; i++) {
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
