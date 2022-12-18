using System;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.Behaviours {
    public class ArcherBehaviour : ICharacter, ISingleShootCharacter, IUltAbilityCharacter{

        public float Health { get; set; }

        public CharacterType Type => CharacterType.Archer;

        public string CharacterPrefabName { get; set; }

        public float DamageValue { get; set; }

        public float UltDamageValue { get; set; }

        public event Action OnDied;

        public ArcherBehaviour(float health, string characterPrefabName, float damageValue, float ultDamageValue) {
            Health = health;
            CharacterPrefabName = characterPrefabName;
            DamageValue = damageValue;
            UltDamageValue = ultDamageValue;
        }

        public void SetHealth(float health) {
            throw new System.NotImplementedException();
        }

        public void Died() {
            OnDied?.Invoke();
        }

        public void ShootTo(ICharacter character) {
            throw new System.NotImplementedException();
        }

        public void UseUlt() {
            throw new System.NotImplementedException();
        }
    }
}
