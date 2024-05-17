using RLNET;
using RLNETConsoleGame.Interfaces;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Core
{
    public class Item : IItem , ITreasure, IDrawable
    {
        public Item()
        {
            Symbol = '!';
            Color = RLColor.Yellow;
        }

        public string Name { get; protected set; }
        public int RemainingUses { get; protected set; }

        public bool Use()
        {
            return UseItem();
        }

        protected virtual bool UseItem()    //this method is protected and can only be seen or used by classes in the method or that inherit the method 
        {
            return false;
        }

        public bool PickUp(IActor actor)
        {
            Player player = actor as Player;
            if (player != null)
            {
                if (player.AddItem(this))
                {
                    Game.MessageLog.Add($"{actor.Name} picked up {Name}");
                    return true;

                }
            }

            return false;
        }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }

            if (map.IsExplored(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, RLColor.Blend(Color, RLColor.Gray, 0.5f), Colors.FloorBackground, Symbol);
            }
        }
    }
}
