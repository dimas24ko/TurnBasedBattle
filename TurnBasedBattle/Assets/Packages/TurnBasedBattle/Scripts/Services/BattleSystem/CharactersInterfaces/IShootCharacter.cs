using System;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces
{
    public interface IShootCharacter {
        public float DamageValue { get; set; }

        public event Action<ICharacter> OnShoot;
    }
}
