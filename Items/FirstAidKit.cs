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
    public class FirstAidKit : Item
    {
        public FirstAidKit() 
        {
            Name = "First Aid Kit";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            int HealAmount = 20;

            Game.MessageLog.Add($"{Game.Player.Name} is using a {Name} . {HealAmount} health gained");

            Heal heal = new Heal(HealAmount);

            RemainingUses--;

            return heal.Perform();
        }
    }
}
