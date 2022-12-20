using System;
using TMPro;
using TurnBasedBattle.Scripts.Services.BattleSystem.Behaviours;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.ViewScripts {
    public class CharacterInfo : MonoBehaviour {
        public ICharacter character;
        public CharacterSimpleAnimator Animator;
        public TextMeshPro HealthValue;

        public bool IsSelectable;
        public bool IsUsedInTurn;
        public bool IsUltReady { get; private set; }

        public Action<CharacterInfo> OnCharacterSelected;

        private void Start() {
            character.OnDied += DestroyCharacter;
            character.OnHealthChanged += ChangeHealth;

            ChangeHealth(character.Health);
            SetRandomColor();
            SubscribeOnCharacterType();
        }

        public void SetUltFalse() =>
            IsUltReady = false;

        private void SetRandomColor() =>
            Animator.SetColor();

        private void SubscribeOnCharacterType() {
            switch (character.Type) {
                case CharacterType.Archer:
                    var archer = (ArcherBehaviour)character;
                    archer.OnShoot += (shootCharacter) => Animator.SimpleShootAnimation(shootCharacter.CharacterPrefab.transform.position);
                    archer.OnUltReady += () => IsUltReady = true;
                    break;
                case CharacterType.Magician:
                    break;
                case CharacterType.Doctor:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DestroyCharacter() =>
            Animator.DiedAnimation();

        private void OnMouseDown() =>
            Select();

        private void ChangeHealth(float newValue) =>
            HealthValue.text = newValue.ToString();

        private void Select() {
            if (!IsSelectable) {
                return;
            }

            OnCharacterSelected?.Invoke(this);
        }
    }
}
