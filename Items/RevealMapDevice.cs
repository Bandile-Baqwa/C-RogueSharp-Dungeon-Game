using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.DiceNotation;
using RLNETConsoleGame.Equipment;
using RLNETConsoleGame.Systems;
using RLNETConsoleGame.Core;
using RogueSharp;


namespace RLNETConsoleGame.Items
{
    public class RevealMapDevice : Item
    {
        public RevealMapDevice()
        {
            Name = "GPS Device";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            DungeonMap map = Game.DungeonMap;

            Game.MessageLog.Add($"{Game.Player.Name} pull up the {Name} so we can scope the area");

            foreach (Cell cell in map.GetAllCells())
            {
                if (cell.IsWalkable)
                {
                    map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }

            RemainingUses--;

            return true;
        }


    }
}
