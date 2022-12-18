using System.Collections.Generic;
using Newtonsoft.Json;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData
{
    public class CharactersDataContainer: IInitializable {
        public Dictionary<CharacterType, CharacterData> CharactersDataMap;

        private JsonDataContainer _jsonDataContainer;

        [Inject]
        public CharactersDataContainer(JsonDataContainer jsonDataContainer) {
            _jsonDataContainer = jsonDataContainer;
        }

        public CharacterData GetDataByType(CharacterType type) {
            return CharactersDataMap[type];
        }

        public void Initialize() {
            CharactersDataMap =
                JsonConvert.DeserializeObject<Dictionary<CharacterType, CharacterData>>(_jsonDataContainer.CharactersSettings.text);
        }
    }
}
