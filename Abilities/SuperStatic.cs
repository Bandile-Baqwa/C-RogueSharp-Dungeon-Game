using RLNETConsoleGame.Core;
using RLNETConsoleGame.Interfaces;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Abilities
{
    public class SuperStatic : Ability, ITargetable
    {
        private readonly int _attack;
        private readonly int _attackChance;

        public SuperStatic(int attack, int attackChance)
        {
            Name = "Super Static";
            TurnsToRefresh = 40;
            TurnsUntilRefreshed = 0;
            _attack = attack;
            _attackChance = attackChance;
        }

        protected override bool PerformAbility()
        {
            return Game.TargetingSystem.SelectLine(this);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;
            Game.MessageLog.Add($"Electrifying !! {player.Name} casts a {Name}");
            Actor superStatic = new Actor
            {
                Attack = _attack,
                AttackChance  = _attackChance,
                Name = Name
            };
                                                    // start of line       end of line
            foreach(Cell cell in map.GetCellsAlongLine(player.X, player.Y, target.X, target.Y))
            {
                if (cell.IsWalkable)
                {
                    continue;
                }
                if (cell.X == player.X && cell.Y == player.Y)
                {
                    continue;
                }

                Monster monster = map.GetMonsterAt(cell.X, cell.Y);
                if (monster != null)
                {
                    Game.CommandSystem.Attack(superStatic, monster);
                }
                else
                {
                    //We hit a wall so the bolt stops 
                    // TODO : with update add code that will make static bolts destroy walls or doors
                    return;
                }
            }
        }
    }
}
