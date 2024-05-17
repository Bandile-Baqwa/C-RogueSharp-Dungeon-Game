﻿using RLNETConsoleGame.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Abilities
{
    public class RevealMap : Ability
    {
        private readonly int _revealDistance;
        public RevealMap(int revealDistance)
        {
            Name = "Reveal Map";
            TurnsToRefresh = 100;
            TurnsUntilRefreshed = 0;
            _revealDistance = revealDistance;
        }

        protected override bool PerformAbility()
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;

            foreach (Cell cell in map.GetCellsInCircle(player.X, player.Y, _revealDistance))
            {
                if (cell.IsWalkable)
                {
                    map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
;
            return true;
        }
    }
}