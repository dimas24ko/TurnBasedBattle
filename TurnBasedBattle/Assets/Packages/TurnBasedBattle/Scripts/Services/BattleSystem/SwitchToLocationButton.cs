using UnityEngine;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem {
    public class SwitchToLocationButton : MonoBehaviour {
        private SceneSwitcher _sceneSwitcher;

        [Inject]
        public void Construct(SceneSwitcher sceneSwitcher) =>
            _sceneSwitcher = sceneSwitcher;

        public void LoadLocation() =>
            _sceneSwitcher.LoadGame();
    }
}
