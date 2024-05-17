using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Equipment
{
    public class BodyEquipment  : Core.Equipment    //this means tthat BodyEquipment is a subclass of Equipment in the Core namespace
    {
        public static BodyEquipment None()
        {
            return new BodyEquipment { Name = "None"};
        }

        public static BodyEquipment PaddedClothing()        //these are predifined Equipment items 
        {
            return new BodyEquipment()
            {
                Defense = 1,
                DefenseChance = 5,
                Name = "Padded Clothing"
            };
        }

        public static BodyEquipment Level1BVest()
        {
            return new BodyEquipment()
            {
                Defense = 1,
                DefenseChance = 10,
                Name = "Level1 Bullet Proof Vest"
            };
        }
        public static BodyEquipment Level2BVest()
        {
            return new BodyEquipment()
            {
                Defense = 2,
                DefenseChance = 10,
                Name = "Level2 Bullet Proof Vest"
            };
        }
        public static BodyEquipment Level3BVest()
        {
            return new BodyEquipment()
            {
                Defense = 2,
                DefenseChance = 15,
                Name = "Level3 Bullet Proof Vest"
            };
        }
    }
}
