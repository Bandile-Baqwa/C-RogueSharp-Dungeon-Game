using RLNET;
using RLNETConsoleGame.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Core
{
    public class PlayerInventory    : Player
    {
        //private List<Item> playerInventoryList;
        //public PlayerInventory()
        //{
        //    playerInventoryList = new List<Item>()      // the list that all the bought items will be sent to 
        //    {
        //        new Item("Skeleton Key",0,1)
        //    };
        //}
        //public void DisplayPlayerInventory(RLConsole inventoryConsole)
        //{
        //    int y = 1;
        //    foreach (var item in playerInventoryList)
        //    {

        //        inventoryConsole.Print(1, y, $" Player Inventory :{item}", Colors.Text);
        //        y += 2;
        //    }
        //}

        //public bool HasEnoughGold(int goldCost)
        //{
        //    return PlayerGold >= goldCost;
        //}

        //public void DeductGold(int goldCost)
        //{
        //    PlayerGold -= goldCost;
        //}

        //public void AddToInventory(Item item)
        //{
        //    playerInventoryList.Add(item);
        //    //add code that will link the buy method and adds the specific item that will be used in the key if statements in game.Cs 
        //    //the if game statments will just call on this method instead of having those if statements hold to much code and functuonality
        //}
    }
}
