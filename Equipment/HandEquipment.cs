using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Systems;


namespace RLNETConsoleGame.Equipment
{
    public class HandEquipment  : Core.Equipment
    {
        public static HandEquipment None()
        {
            return new HandEquipment { Name = "None" };
        }

        public static HandEquipment Dagger()
        {
            return new HandEquipment()
            {
                Attack = 1,
                AttackChance = 10,
                Name = "Dagger",
                Speed = -2
            };
        }

        public static HandEquipment Katana()
        {
            return new HandEquipment()
            {
                Attack = 1,
                AttackChance = 20,
                Name = "Katana"
            };
        }

        public static HandEquipment DoubleKatana()
        {
            return new HandEquipment()
            {
                Attack = 3,
                AttackChance = 30,
                Name = "Double Katana",
                Speed = 3
            };
        }

        public static HandEquipment Glock()
        {
            return new HandEquipment()
            {
                Attack = 1,
                AttackChance = 18,
                Name = "Double Katana",
                Speed = 5
            };
        }

        public static HandEquipment AR15()
        {
            return new HandEquipment()
            {
                Attack = 5,
                AttackChance = 30,
                Name = "Double Katana",
                Speed = 6
            };
        }
    }
}
