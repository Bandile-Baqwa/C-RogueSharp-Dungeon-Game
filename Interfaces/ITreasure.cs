﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;

namespace RLNETConsoleGame.Interfaces
{
    public interface ITreasure
    {
        bool PickUp(IActor actor);
    }
}
