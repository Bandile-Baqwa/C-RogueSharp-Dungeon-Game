using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Systems;

namespace RLNETConsoleGame.Equipment
{
    public class HeadEquipment : Core.Equipment
    {
        public static HeadEquipment None()
        {
            return new HeadEquipment { Name = "None" };
        }

        public static HeadEquipment SkiiMask() 
        {
            return new HeadEquipment()
            {
                Defense = 1,
                DefenseChance = 5,
                Name = "Skii Mask"                      //these names will be inserted into the MessageLog.Add
            };
        }

        public static HeadEquipment HockeyMask()
        {
            return new HeadEquipment()
            {
                Defense = 1,
                DefenseChance = 10,
                Name = "Hockey Mask"
            };
        }

        public static HeadEquipment BallisticMask()
        {
            return new HeadEquipment()
            {
                Defense = 1,
                DefenseChance = 15,
                Name = "Ballistic Mask"
            };
        }

    }
}
