using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.DiceNotation;
using RLNETConsoleGame.Equipment;
using RLNETConsoleGame.Abilities;
using RLNETConsoleGame.Systems;
using RLNETConsoleGame.Core;
using RogueSharp;

namespace RLNETConsoleGame.Items
{
    public class BlackAirFOne   : Item
    {
        public BlackAirFOne()
        {
            Name = "Black Air Forces 1's";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            Monster monster = new Monster();
            int SpeedBoost = 5;
            int MonsterAwareness = 3;

            Game.MessageLog.Add($"Oh Snap!, {Game.Player.Name} just put on {Name}, instant Speed boost and they gonna try avoid {Game.Player.Name}");

            Game.Player.Speed += SpeedBoost;
            monster.Speed -= MonsterAwareness;

            RemainingUses--;

            return true;
        }
    }
}
