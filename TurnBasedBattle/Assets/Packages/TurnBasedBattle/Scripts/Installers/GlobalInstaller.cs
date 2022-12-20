using TurnBasedBattle.Scripts.Services;
using TurnBasedBattle.Scripts.Services.BattleSystem;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharacterFactory;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.Generations;
using TurnBasedBattle.Scripts.Services.Common;
using TurnBasedBattle.Scripts.Services.PrefabLoader;
using Zenject;

namespace TurnBasedBattle.Scripts.Installers {
    public class GlobalInstaller : MonoInstaller {
        public JsonDataContainer JsonDataContainer;

        public override void InstallBindings() {
            InstallCommonData();
            InstallCharactersData();
            InstallPrefabLoader();

            InstallSceneSwitcher();
            InstallLifeCycle();
        }

        private void InstallLifeCycle() =>
            Container.Bind<GameLifeCycle>().AsSingle();

        private void InstallSceneSwitcher() =>
            Container.Bind<SceneSwitcher>().AsSingle();

        private void InstallPrefabLoader() =>
            Container.Bind<PrefabLoader>().AsSingle();

        private void InstallCharactersData() {
            Container.Bind<CharactersContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharactersDataContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<SimpleCharacterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharactersGenerator>().AsSingle();
        }

        private void InstallCommonData() {
            Container.Bind<JsonDataContainer>().FromScriptableObject(JsonDataContainer).AsSingle();
        }
    }
}
