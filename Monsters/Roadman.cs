using RLNETConsoleGame.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Monsters
{
    public class Roadman : Monster
    {
        public static Roadman Create(int mapLevel)
        {
            int health = Dice.Roll("2D5");

            return new Roadman
            {
                Attack = Dice.Roll("1D3") + mapLevel / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.RoadmanColor,
                Defense = Dice.Roll("1D3") + mapLevel / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Roadman",
                Speed = 15,
                Symbol = 'K'
            };
        }
    }
}
