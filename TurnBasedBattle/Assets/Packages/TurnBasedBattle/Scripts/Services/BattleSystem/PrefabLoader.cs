using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TurnBasedBattle.Scripts.Services.BattleSystem {
    public class PrefabLoader {
        public async Task<GameObject> LoadCharacterPrefab(string prefabName) {
             return await Addressables.LoadAssetAsync<GameObject>(prefabName).Task;
        }
    }
}
