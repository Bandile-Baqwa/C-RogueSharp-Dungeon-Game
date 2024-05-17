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
    public class MolotovCocktail : Ability, ITargetable
    {
        private readonly int _attack;
        private readonly int _attackChance;
        private readonly int _area;

        public MolotovCocktail(int attack, int attackChance, int area)
        {
            Name = "Molotov Cocktail";
            TurnsToRefresh = 40;
            TurnsUntilRefreshed = 0;
            _attack = attack;
            _attackChance = attackChance;
            _area = area;
        }

        protected override bool PerformAbility()
        {
            return Game.TargetingSystem.SelectArea(this, _area);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;

            Game.MessageLog.Add($"{player.Name} our here playing with liquor and throws a {Name}");

            Actor molotovCocktail = new Actor
            {
                Attack = _attack,
                AttackChance = _attackChance,
                Name = Name
            };

            foreach (Cell cell in map.GetCellsInCircle(target.X, target.Y,_area))
            {
                Monster monster = map.GetMonsterAt(cell.X, cell.Y);

                if (monster != null)
                {
                    Game.CommandSystem.Attack(molotovCocktail, monster);
                }
            }
        }
    }
}
