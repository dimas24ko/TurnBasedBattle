using System;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces {
    public interface IUltAbilityCharacter<T> {
        public float UltDamageValue { get; set; }
        public void UseUlt(T value);

        public event Action OnUltUsed;
    }
}
