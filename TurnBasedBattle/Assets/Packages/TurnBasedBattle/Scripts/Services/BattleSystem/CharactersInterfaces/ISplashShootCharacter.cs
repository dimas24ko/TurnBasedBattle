using System.Collections.Generic;

namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces
{
    public interface ISplashShootCharacter : IShootCharacter {
        public void SplashShoot(List<ICharacter> character);
    }
}
