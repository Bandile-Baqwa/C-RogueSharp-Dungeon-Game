using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        int RemainingUses { get; }

        bool Use();
    }
}
