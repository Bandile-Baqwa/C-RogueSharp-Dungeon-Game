using RLNETConsoleGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Abilities
{
    public class Heal : Ability
    {
        private readonly int _amountToHeal; 

        public Heal(int amountToHeal)
        {
            Name = "Heal";
            TurnsToRefresh = 20;
            TurnsUntilRefreshed = 0;
            _amountToHeal = amountToHeal;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;

            player.Health = Math.Min(player.MaxHealth, player.Health + _amountToHeal);

            return true;
        }
    }
}
