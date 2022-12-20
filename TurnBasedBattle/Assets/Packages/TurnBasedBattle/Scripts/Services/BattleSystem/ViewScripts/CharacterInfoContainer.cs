using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts
{
    public class CharacterInfoContainer : MonoBehaviour {
        public CharactersPrefabCreator CharactersPrefabCreator;

        public List<CharacterInfo> PlayerCharacters;
        public List<CharacterInfo> EnemyCharacters;

        private void Awake() =>
            CharactersPrefabCreator.OnPrefabsCreated += SubscribeOnCharacterAction;

        private void SubscribeOnCharacterAction()
        {
            foreach (CharacterInfo characterInfo in PlayerCharacters)
            {
                characterInfo.character.OnDied += () => PlayerCharacters.Remove(characterInfo);
            }

            foreach (CharacterInfo characterInfo in EnemyCharacters)
            {
                characterInfo.character.OnDied += () => EnemyCharacters.Remove(characterInfo);
            }
        }
    }
}
