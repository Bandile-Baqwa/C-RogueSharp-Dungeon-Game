using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Items;

namespace RLNETConsoleGame.Systems
{
    public static class ItemGenerator
    {
        public static Item CreateItem()
        {
            Pool<Item> itemPool = new Pool<Item>();

            itemPool.Add(new ArmorScroll(), 10);
            itemPool.Add(new Bazooka(), 5);
            itemPool.Add(new FirstAidKit(), 20);
            itemPool.Add(new RevealMapDevice(), 25);
            itemPool.Add(new TeleportScroll(), 20);
            itemPool.Add(new Whetstone(), 10);
            itemPool.Add(new BlackAirFOne(), 10);

            return itemPool.Get();
        }
    }
}
