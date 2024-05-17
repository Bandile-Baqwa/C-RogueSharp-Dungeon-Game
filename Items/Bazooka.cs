using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RogueSharp.Random;
using RogueSharp;
using RLNETConsoleGame.Systems;

namespace RLNETConsoleGame.Items
{
    public class Bazooka   : Item
    {
        public Bazooka()
        {
            Name = "Bazooka";
            RemainingUses = 3;
        }

        protected override bool UseItem()
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;
            Point edgePoint = GetRandomEdgePoint(map);

            Game.MessageLog.Add($"BOOOOOM!!, {player.Name} used a {Name}, that will show them that {player.Name} means business ");

            Actor voidAttackActor = new Actor
            {
                Attack = 6,
                AttackChance = 90,
                Name = "BOOOOM"
            };

            Cell previousCell = null;
            foreach (Cell cell in map.GetCellsAlongLine(player.X, player.Y, edgePoint.X, edgePoint.Y))
            {
                if (cell.X == player.X && cell.Y == player.Y)
                {
                    continue;
                }

                Monster monster = map.GetMonsterAt(cell.X, cell.Y);
                if (monster != null) 
                {
                    Game.CommandSystem.Attack(voidAttackActor, monster);
                }
                else
                {
                    map.SetCellProperties(cell.X, cell.Y, true, true, true);
                    if (previousCell != null)
                    {
                        if (cell.X != previousCell.X || cell.Y != previousCell.Y)
                        {
                            map.SetCellProperties(cell.X + 1, cell.Y, true, true, true);
                        }
                    }
                    previousCell = cell;

                }
            }
            RemainingUses--;

            return true;
        }

        private Point GetRandomEdgePoint(DungeonMap map)
        {
            var random = new DotNetRandom();

            int result = random.Next(1, 4);
            switch (result)     // the switch is gonna use the random number thats generated and decid which edge to place the point 
            {
                //the random number will be between 1 - 4 based on the output the switch will chose the repective case 

                //Random.Next(3, map.Width... in each case this will generate a number between 3 and map height/width -3 to avoid the point 
                //being placed too clsoe to the corner 
                case 1: //Top
                    {
                        return new Point(random.Next(3, map.Width - 3), 3); 
                    }
                case 2: //Bottom
                    {
                        return new Point(random.Next(3, map.Width - 3), map.Height - 3);
                    }
                case 3: //Right
                    {
                        return new Point(map.Width - 3, random.Next(3, map.Height - 3));
                    }
                case 4: //Left
                    {
                        return new Point(3, random.Next(3, map.Height - 3));
                    }
                default:    //if by any chance 1 - 4 isnt returned the point will be placed at 3,3 
                    {
                        return new Point(3, 3);
                    }
            }
        }
    }
}
