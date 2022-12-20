using System.Collections.Generic;
using Newtonsoft.Json;
using TurnBasedBattle.Scripts.Services.Common;
using UnityEngine;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData
{
    public class CharactersDataContainer: IInitializable {
        private Dictionary<CharacterType, CharacterData> _charactersDataMap;

        private JsonDataContainer _jsonDataContainer;

        [Inject]
        public CharactersDataContainer(JsonDataContainer jsonDataContainer) =>
            _jsonDataContainer = jsonDataContainer;

        public CharacterData GetDataByType(CharacterType type) {
            return _charactersDataMap.ContainsKey(type)
                ? _charactersDataMap[type]
                : new CharacterData();
        }

        public void Initialize() =>
            _charactersDataMap = JsonConvert.DeserializeObject<Dictionary<CharacterType, CharacterData>>(_jsonDataContainer.CharactersSettings.text);
    }
}
