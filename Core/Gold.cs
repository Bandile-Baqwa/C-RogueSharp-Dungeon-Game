﻿using RLNET;
using RLNETConsoleGame.Interfaces;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Core
{
    public class Gold :ITreasure,IDrawable
    {
        public int Amount { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Gold(int amount)
        {
            Amount = amount;
            Symbol = '$';
            Color = RLColor.Yellow;
        }

        public bool PickUp(IActor actor)
        {
            actor.Gold += Amount;
            Game.MessageLog.Add($"YEEEEESIR !! {actor.Name} picked up {Amount} gold");
            return true;
        }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(X,Y))
            {
                return;
            }
            if (map.IsInFov(X,Y))
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
