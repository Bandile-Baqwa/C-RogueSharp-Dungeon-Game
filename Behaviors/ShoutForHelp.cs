using RLNETConsoleGame.Interfaces;
using RLNETConsoleGame.Systems;
using RLNETConsoleGame.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Behaviors
{
    public class ShoutForHelp : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            bool didShoutForHelp = false;
            DungeonMap dungeonMap = Game.DungeonMap;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
            foreach (var monsterLocation in dungeonMap.GetMonsterLocations())
            {
                if (monsterFov.IsInFov(monsterLocation.X, monsterLocation.Y))
                {
                    Monster alertedMonster = dungeonMap.GetMonsterAt(monsterLocation.X, monsterLocation.Y);
                    if (!alertedMonster.TurnsAlerted.HasValue)
                    {
                        alertedMonster.TurnsAlerted = 1;
                        didShoutForHelp = true;
                    }
                }
            }

            if (didShoutForHelp)
            {
                Game.MessageLog.Add($"{monster.Name} shouts for help!");
            }

            return didShoutForHelp;
        }
    }
}
