namespace TurnBasedBattle.Scripts.Services.BattleSystem.CharactersInterfaces {
    public interface IUltAbilityCharacter {
        public float UltDamageValue { get; set; }
        public void UseUlt();
    }
}
