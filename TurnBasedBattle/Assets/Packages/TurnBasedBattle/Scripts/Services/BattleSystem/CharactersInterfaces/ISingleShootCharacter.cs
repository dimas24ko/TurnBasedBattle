namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces
{
    public interface ISingleShootCharacter : IShootCharacter {
        public void ShootTo(ICharacter character);
    }
}
