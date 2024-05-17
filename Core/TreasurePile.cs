using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Interfaces;

namespace RLNETConsoleGame.Core
{
    public class TreasurePile
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ITreasure Treasure { get; set; }

        public TreasurePile(int x, int y, ITreasure treasure)        {
            X = x;
            Y = y;
            Treasure = treasure;

            IDrawable drawableTreasure = treasure as IDrawable;      //using as for a safe cast incase a null is return to prevent a Exception
                                       //oject treasure is gonna be tried to be casted to IDrawable
            if (drawableTreasure != null)
            {
                drawableTreasure.X = x;     //X and Y will be set if the cast is successful (treasure must be in an instance that implements IDrawable)
                drawableTreasure.Y = y;
            }
        }

    }
}
