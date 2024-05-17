using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Systems;

namespace RLNETConsoleGame.Equipment
{
    public class FeetEquipment : Core.Equipment     //Core.Equipment is inherited cause of the pool of <Core.Equipment> in Equipment generator
    {
        public static FeetEquipment None() 
        {
            return new FeetEquipment { Name = "None" };
        }

        public static FeetEquipment Sneakers()
        {
            return new FeetEquipment()
            {
                Defense = 1,
                DefenseChance = 5,
                Name = "Sneakers"
            };
        }

        public static FeetEquipment MetalPlatedShoes()
        {
            return new FeetEquipment()
            {
                Defense = 1,
                DefenseChance = 10,
                Name = "Metal Platted Shoes"
            };
        }

        public static FeetEquipment BallisticShoes()
        {
            return new FeetEquipment()
            {
                Defense = 1,
                DefenseChance = 15,
                Name = "Ballistic Shoes"
            };
        }

    }
}
