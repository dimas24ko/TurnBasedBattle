using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.UI
{
    public class CharacterPanel : MonoBehaviour {
        public GameObject Panel;
        public GameObject SimpleShootButton;
        public GameObject UltShootButton;

        public void Open(bool ultButtonActive) {
            Panel.SetActive(true);
            SimpleShootButton.SetActive(true);
            UltShootButton.SetActive(ultButtonActive);
        }

        public void Hide() =>
            Panel.SetActive(false);
    }
}
