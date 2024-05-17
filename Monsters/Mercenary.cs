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
    public class Mercenary    : Monster
    {
        private int? _turnsSpentRunning = null;
        private bool _shoutedForHelp = false;

        public static Mercenary Create(int mapLevel)
        {
            int health = Dice.Roll("1D5");
            return new Mercenary
            {
                Attack = Dice.Roll("1D2") + mapLevel / 3,
                AttackChance = Dice.Roll("10D5"),
                Awareness = 15,
                Color = Colors.MercenaryColor,
                Defense = Dice.Roll("1D2") + mapLevel / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("1D20"),
                Health = health,
                MaxHealth = health,
                Name = "Mercenary",
                Speed = 17,
                Symbol = 'm'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var fullyHealBehavior = new FullyHeal();
            var runAwayBehavior = new RunAway();
            var shoutForHelpBehavior = new ShoutForHelp();

            if (_turnsSpentRunning.HasValue && _turnsSpentRunning.Value > 15)
            {
                fullyHealBehavior.Act(this, commandSystem);
                _turnsSpentRunning = null;
            }
            else if (Health < MaxHealth)
            {
                runAwayBehavior.Act(this, commandSystem);
                if (_turnsSpentRunning.HasValue)
                {
                    _turnsSpentRunning += 1;
                }
                else
                {
                    _turnsSpentRunning = 1;
                }

                if (!_shoutedForHelp)
                {
                    _shoutedForHelp = shoutForHelpBehavior.Act(this, commandSystem);
                }
            }

            else
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
