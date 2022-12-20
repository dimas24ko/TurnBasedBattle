using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.Common {
    [CreateAssetMenu(fileName = "JsonDataContainer", menuName = "Data/JsonDataContainer", order = 0)]
    public class JsonDataContainer : ScriptableObject {
        public TextAsset CharactersLinesMap;
        public TextAsset CharactersSettings;
    }
}
