using TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine;
using TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine.States;
using Zenject;

namespace TurnBasedBattle.Scripts.Installers {
    public class GameplaySceneInstaller : MonoInstaller {

        public TurnContext TurnContext;

        public override void InstallBindings() {

            Container.Bind<TurnContext>().FromInstance(TurnContext).AsSingle();

            InstallStateMachine();
        }

        private void InstallStateMachine() =>
            Container.Bind<GameplayStateMachine>().AsSingle();
    }
}
