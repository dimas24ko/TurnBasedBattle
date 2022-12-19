using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TurnBasedBattle.Scripts.Services.BattleSystem {
    public class PrefabLoader {
        public GameObject LoadCharacterPrefab(string prefabName) {
            return Addressables.LoadAssetAsync<GameObject>(prefabName).Result;
        }
    }
}
