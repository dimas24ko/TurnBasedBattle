using UnityEngine;

namespace TurnBasedBattle.Scripts.Services {
    public class SetActionTypeButton : MonoBehaviour {
        public CharacterActionExecutor CharacterActionExecutor;
        public ActionType Type;

        public void SetType() =>
            CharacterActionExecutor.SetActionType(Type);
    }
}
