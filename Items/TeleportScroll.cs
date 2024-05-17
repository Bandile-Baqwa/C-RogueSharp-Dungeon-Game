using RLNETConsoleGame.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.DiceNotation;
using RLNETConsoleGame.Equipment;
using RLNETConsoleGame.Systems;

namespace RLNETConsoleGame.Items
{
    public class TeleportScroll : Item
    {
        public TeleportScroll()
        {
            Name = "Teleport Scroll";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;

            Game.MessageLog.Add($" POOOOFFF! them goons aint see nothing , {player.Name} used a {Name} and reappeared in another place");

            Point point = map.GetRandomLocation();

            map.SetActorPosition(player, point.X, point.Y);

            RemainingUses--;

            return true;
        }
    }
}
