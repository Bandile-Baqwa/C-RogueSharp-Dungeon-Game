using RLNETConsoleGame.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Abilities
{
    public class DoNothing : Ability
    {
        public DoNothing()
        {
            Name = "Nothing";
            TurnsToRefresh = 0;
            TurnsUntilRefreshed = 0;
        }

        protected override bool PerformAbility()
        {
            Game.MessageLog.Add("No ability in that slot");
            return false;
        }
    }
}
