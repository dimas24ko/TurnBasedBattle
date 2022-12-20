using System;
using System.Collections.Generic;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services {
    public class CharactersPanelsSwitcher : MonoBehaviour {
        public CharacterPanel ArcherPanel;

        private Dictionary<CharacterType, CharacterPanel> CharactersPanelsMap = new Dictionary<CharacterType, CharacterPanel>();

        private void Awake() =>
            CharactersPanelsMap.Add(CharacterType.Archer, ArcherPanel); // Redev on normal serializations dictionary with Odin

        public void ShowPanel(CharacterType characterType, bool ultButtonActive = false) =>
            CharactersPanelsMap[characterType].Open(ultButtonActive);

        public void HidePanel(CharacterType characterType) =>
            CharactersPanelsMap[characterType].Hide();
    }
}
