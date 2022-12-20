using System;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces {
    public interface IUltAbilityCharacter<in T> {
        public float UltDamageValue { get; }
        public void UseUlt(T value);

        public event Action OnUltUsed;
    }
}
