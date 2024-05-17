using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Equipment;

namespace RLNETConsoleGame.Systems
{
    public class EquipmentGenerator
    {
        private readonly Pool<Core.Equipment> _equipmentPool;

        public EquipmentGenerator(int mapLevel)
        {
            _equipmentPool = new Pool<Core.Equipment>();

            if (mapLevel <= 3)
            {
                _equipmentPool.Add(BodyEquipment.PaddedClothing(), 20);
                _equipmentPool.Add(HeadEquipment.SkiiMask(), 20);
                _equipmentPool.Add(FeetEquipment.Sneakers(), 20);
                _equipmentPool.Add(HandEquipment.Dagger(), 25);
                _equipmentPool.Add(BodyEquipment.Level1BVest(), 5);
            }
            else if (mapLevel <= 6)
            {
                _equipmentPool.Add(BodyEquipment.Level1BVest(), 20);
                _equipmentPool.Add(HeadEquipment.HockeyMask(), 20);
                _equipmentPool.Add(FeetEquipment.MetalPlatedShoes(), 20);
                _equipmentPool.Add(HandEquipment.Katana(), 15);
                _equipmentPool.Add(HandEquipment.Glock(), 20);
                _equipmentPool.Add(BodyEquipment.Level2BVest(), 5);
            }
            else
            {
                _equipmentPool.Add(BodyEquipment.Level2BVest(), 20);
                _equipmentPool.Add(BodyEquipment.Level3BVest(), 25);
                _equipmentPool.Add(HeadEquipment.BallisticMask(), 25);
                _equipmentPool.Add(FeetEquipment.BallisticShoes(), 25);
                _equipmentPool.Add(HandEquipment.AR15(), 25);
                _equipmentPool.Add(HandEquipment.DoubleKatana(), 25);

            }
        }

        public Core.Equipment CreateEquipment()
        {
            return _equipmentPool.Get();
        }
    }
}
