using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.DiceNotation;
using RLNETConsoleGame.Equipment;
using RLNETConsoleGame.Systems;
using RLNETConsoleGame.Core;
using RogueSharp;

namespace RLNETConsoleGame.Items
{
    public class NoItem : Item
    {
        public NoItem() 
        {
            Name = "None";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            return false;
        }
    }
}
