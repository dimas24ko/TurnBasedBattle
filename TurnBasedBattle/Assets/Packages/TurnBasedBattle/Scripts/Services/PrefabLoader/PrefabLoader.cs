using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TurnBasedBattle.Scripts.Services.PrefabLoader {
    public class PrefabLoader {
        public async Task<GameObject> LoadCharacterPrefab(string prefabName) {
             return await Addressables.LoadAssetAsync<GameObject>(prefabName).Task;
        }
    }
}
