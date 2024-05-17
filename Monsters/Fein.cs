using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RogueSharp;
using RogueSharp.DiceNotation;

namespace RLNETConsoleGame.Monsters
{
    public class Fein : Monster
    {
        public static Fein Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Fein
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.FeinColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Fein",
                Speed = 14,
                Symbol = 'K'
            };
        }
    }
}