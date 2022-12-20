using TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.UI {
    public class SetActionTypeButton : MonoBehaviour {
        public CharacterActionExecutor CharacterActionExecutor;
        public ActionType Type;

        public void SetType() =>
            CharacterActionExecutor.SetActionType(Type);
    }
}
