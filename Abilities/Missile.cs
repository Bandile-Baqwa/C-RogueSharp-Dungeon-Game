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
    public class Missile : Ability , ITargetable
    {
        private readonly int _attack;
        private readonly int _attackChance;

        public Missile(int attack, int attackChance)
        {
            Name = "Missile";
            TurnsToRefresh = 10;
            TurnsUntilRefreshed = 0;
            _attack = attack;
            _attackChance = attackChance;
        }

        protected override bool PerformAbility()
        {
            return Game.TargetingSystem.SelectMonster(this);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;
            Monster monster = map.GetMonsterAt(target.X, target.Y); //this is added to get the targeted monsters location

            if (monster != null)
            {
                Game.MessageLog.Add($"{player.Name} shoots a {Name} at a {monster.Name}");

                Actor missileActor = new Actor()
                {
                    Attack = _attack, 
                    AttackChance = _attackChance, 
                    Name = Name
                };

            Game.CommandSystem.Attack(missileActor, monster);

            }
        }
    }
}
