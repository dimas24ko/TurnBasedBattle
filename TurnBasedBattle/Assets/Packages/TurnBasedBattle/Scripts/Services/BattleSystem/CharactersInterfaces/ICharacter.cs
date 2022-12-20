using System;
using TurnBasedBattle.Scripts.Services.BattleSystem.CharactersData;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces
{
    public interface ICharacter {

        public float Health { get; }

        public CharacterType Type { get;}

        public string CharacterPrefabName { get; }

        public GameObject CharacterPrefab{ get; set; }

        public event Action OnDied;
        public event Action<float> OnHealthChanged;

        public void SetHealth(float health);

        public void Died();
    }
}
