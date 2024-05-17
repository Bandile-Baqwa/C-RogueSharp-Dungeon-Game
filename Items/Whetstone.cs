using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Equipment;
using RLNETConsoleGame.Systems;
using RogueSharp.DiceNotation;

namespace RLNETConsoleGame.Items
{
    public class Whetstone : Item
    {
        public Whetstone()
        {
            Name = "Whetstone";
            RemainingUses = 5;
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;

            if (player.Hand == HandEquipment.None())
            {
                Game.MessageLog.Add($"{player.Name} is not holding anything they can sharpen");
            }
            else if (player.AttackChance >= 80)
            {
                Game.MessageLog.Add($" Damm {player.Name} what are you trying to cut , Atoms? {player.Hand.Name} is as sharp as can be");
            }
            else
            {
                Game.MessageLog.Add($"Yeeeesir!! use that {Name} to make that {player.Hand.Name} sharper");
                player.Hand.AttackChance += Dice.Roll("1D3");
                RemainingUses--;
            }
            return true;
        }
    }
}
