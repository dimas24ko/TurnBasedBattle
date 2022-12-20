using System;
using System.Collections.Generic;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.Behaviours {
    public class ArcherBehaviour : ICharacter, ISingleShootCharacter, IUltAbilityCharacter<List<CharactersLine>>{

        public float Health { get; set; }

        public CharacterType Type => CharacterType.Archer;

        public string CharacterPrefabName { get; set; }

        public GameObject CharacterPrefab { get; set; }

        public float DamageValue { get; set; }

        public float UltDamageValue { get; set; }

        private const float NeededUltPoints = 20;
        private float _currentUltPoints;

        public event Action<ICharacter> OnShoot;
        public event Action OnDied;
        public event Action<float> OnHealthChanged;
        public event Action OnUltUsed;
        public Action OnUltReady;

        public ArcherBehaviour(float health, string characterPrefabName, float damageValue, float ultDamageValue) {
            Health = health;
            CharacterPrefabName = characterPrefabName;
            DamageValue = damageValue;
            UltDamageValue = ultDamageValue;
        }

        public void SetHealth(float health) {
            Health -= health;
            OnHealthChanged?.Invoke(Health);

            if (Health<=0) {
                Died();
            }
        }

        public void Died() =>
            OnDied?.Invoke();

        public void ShootTo(ICharacter character) {
            character.SetHealth(DamageValue);
            _currentUltPoints += Math.Abs(DamageValue);

            if (_currentUltPoints >= NeededUltPoints) {
                OnUltReady?.Invoke();
            }

            OnShoot?.Invoke(character);
        }

        public void UseUlt(List<CharactersLine> value) {
            _currentUltPoints = 0;

            foreach (CharactersLine line in value) {
                foreach (ICharacter character in line.CharactersInLine) {
                    character?.SetHealth(UltDamageValue);
                }
            }

            OnUltUsed?.Invoke();
        }
    }
}
