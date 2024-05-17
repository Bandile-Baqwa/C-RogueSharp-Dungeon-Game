using RLNETConsoleGame.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Equipment;

namespace RLNETConsoleGame.Items
{
    public class ArmorScroll : Item
    {
        public ArmorScroll()
        {
            Name = "Armor Enhancer";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;

            if (player.Body == BodyEquipment.None())
            {
                Game.MessageLog.Add($"{player.Name} isn't wearing any body armor to enhance");
            }
            else if (player.Defense >= 8)
            {
                Game.MessageLog.Add($"{player.Name} cannot enhance their {player.Body.Name} it's maxed out");
            }
            else
            {
                Game.MessageLog.Add($"Patch that {player.Body.Name} up, putting that {Name} to good use");
                player.Body.Defense += 1;
                RemainingUses--;
            }

            return true;
        }
    }
}
