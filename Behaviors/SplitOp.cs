using RLNETConsoleGame.Interfaces;
using RLNETConsoleGame.Systems;
using RLNETConsoleGame.Monsters;
using RLNETConsoleGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;

namespace RLNETConsoleGame.Behaviors
{
    public class SplitOp : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            DungeonMap map = Game.DungeonMap;

            //Ops only split when wounded
            if (monster.Health >= monster.MaxHealth)
            {
                return false;
            }

            int halfHealth = monster.MaxHealth / 2;
            if (halfHealth <= 0)
            {
                //health would be too low so abort
                return false;
            }

            Cell cell = FindClosestUnoccupiedCell(map, monster.X, monster.Y);

            if (cell == null)
            {
                //empty cells so abort
                return false;
            }

            //this will make a new op with half the health of the old one
            Op newOp = Monster.Clone(monster) as Op;
            if (newOp != null)
            {
                newOp.TurnsAlerted = 1;
                newOp.X = cell.X;
                newOp.Y = cell.Y;
                newOp.MaxHealth = halfHealth;
                newOp.Health = halfHealth;
                map.AddMonsters(newOp);
                Game.MessageLog.Add($"YOOOO!! the {monster.Name} splits itself into two one some Siamese twin separation");
            }
            else
            {
                return false;
            }

            //this will halve the original ops health 
            monster.MaxHealth = halfHealth;
            monster.Health = halfHealth;

            return true;
        }

        private Cell FindClosestUnoccupiedCell(DungeonMap dungeonMap, int x, int y)
        {
            for (int i = 1; i < 5; i++)
            {
                foreach (Cell cell in dungeonMap.GetBorderCellsInCircle(x, y, i))
                {
                    if (cell.IsWalkable)
                    {
                        return cell;
                    }
                }
            }
            return null;
        }
    }
}
