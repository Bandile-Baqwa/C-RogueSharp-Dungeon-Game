using RLNETConsoleGame.Interfaces;
using RLNETConsoleGame.Systems;
using RLNETConsoleGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;

namespace RLNETConsoleGame.Behaviors
{
    public class RunAway : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;

            //set the cell for the monster and player to walkable so the pathfinder doesnt quit early
            dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
            dungeonMap.SetIsWalkable(player.X, player.Y, true);

            GoalMap goalMap = new GoalMap(dungeonMap);
            goalMap.AddGoal(player.X, player.Y, 0);

            Path path = null;
            try
            {
                path = goalMap.FindPathAvoidingGoals(monster.X, monster.Y);
            }
            catch(PathNotFoundException)
            {
                Game.MessageLog.Add($"{monster.Name} is a little B! and cowers in fear");
            }

            //this resets the cell the monster and player are on back to not walkable
            dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
            dungeonMap.SetIsWalkable(player.X, player.Y, false);

            if (path != null)
            {
                try
                {
                    commandSystem.MoveMonster(monster, path.StepForward());
                }
                catch (NoMoreStepsException)
                {
                    Game.MessageLog.Add($"{monster.Name} is a little B! and cowers in fear");
                }
            }

            return true;
        }
    }
}
