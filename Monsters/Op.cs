using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Systems;
using RLNETConsoleGame.Behaviors;
using RogueSharp;
using RogueSharp.DiceNotation;

namespace RLNETConsoleGame.Monsters
{
    public class Op : Monster
    {
        public static Op Create(int mapLevel)
        {
            int health = Dice.Roll("4D5");
            return new Op
            {
                Attack = Dice.Roll("1D2") + mapLevel / 3,
                AttackChance = Dice.Roll("10D5"),
                Awareness = 10,
                Color = Colors.OpColor,
                Defense = Dice.Roll("1D2") + mapLevel / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("1D20"),
                Health = health,
                MaxHealth = health,
                Name = "Op",
                Speed = 15,
                Symbol = 'o'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var splitOpBehavior = new SplitOp();
            if (!splitOpBehavior.Act(this, commandSystem))
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
