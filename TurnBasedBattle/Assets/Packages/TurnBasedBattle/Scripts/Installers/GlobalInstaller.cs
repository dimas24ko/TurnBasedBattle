using TurnBasedBattle.Scripts.Services;
using TurnBasedBattle.Scripts.Services.BattleSystem;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharacterFactory;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using Zenject;

namespace TurnBasedBattle.Scripts.Installers {
    public class GlobalInstaller : MonoInstaller {
        public JsonDataContainer JsonDataContainer;

        public override void InstallBindings() {
            InstallCommonData();
            InstallCharactersData();
        }

        private void InstallCharactersData() {
            Container.Bind<CharactersContainer>().AsSingle();
            Container.BindInterfacesTo<CharactersDataContainer>().AsSingle();
            Container.BindInterfacesTo<SimpleCharacterFactory>().AsSingle();
            Container.Bind<CharactersGenerator>().AsSingle();
        }

        private void InstallCommonData() {
            Container.Bind<JsonDataContainer>().FromScriptableObject(JsonDataContainer).AsSingle();
        }
    }
}
