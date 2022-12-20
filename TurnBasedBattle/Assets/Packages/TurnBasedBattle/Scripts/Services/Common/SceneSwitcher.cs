using UnityEngine.SceneManagement;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.Common {
    public class SceneSwitcher {
        private const string LobbySceneName = "Lobby";
        private const string GameSceneName = "Game";

        private GameLifeCycle _lifeCycle;

        [Inject]
        public SceneSwitcher(GameLifeCycle lifeCycle) {
            _lifeCycle = lifeCycle;

            //TODO redev on StateMachine
            _lifeCycle.Lose += LoadLobby;
            _lifeCycle.Win += LoadLobby;
        }

        public void LoadLobby() =>
            LoadScene(LobbySceneName);

        public void LoadGame() =>
            LoadScene(GameSceneName);

        private void LoadScene(string sceneName) =>
            SceneManager.LoadScene(sceneName);
    }
}
