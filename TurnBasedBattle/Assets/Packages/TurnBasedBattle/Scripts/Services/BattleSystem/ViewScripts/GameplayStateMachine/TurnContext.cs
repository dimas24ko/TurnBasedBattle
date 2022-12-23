using TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.ViewData;
using UnityEngine;
using Zenject;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts.GameplayStateMachine {
    public class TurnContext {
        public CharacterInfoContainer CharacterInfoContainer;

        public CharacterInfo FirstCharacter;
        public CharacterInfo SecondCharacter;

        public ActionType Action;

        public bool IsPlayerTurn;

        public static TurnContext Instance;

        [Inject]
        public TurnContext(CharacterInfoContainer characterInfoContainer) {
            Instance = this;
            CharacterInfoContainer = characterInfoContainer;
        }
    }
}
