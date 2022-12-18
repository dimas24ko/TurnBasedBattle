using System;
using UnityEngine;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces
{
    public interface IMovingCharacter {

        public event Action<Vector3> MoveTo;

        public void Move(Vector3 point);
    }
}
