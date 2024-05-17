using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Interfaces
{
    public interface IAbility
    {
        string Name { get; }            //no setter (ReadOnly) is used for data integrity or encapsulation, so that the only place it can be "set" or changed 
                                        // is where you specify it to change or be set mainly in the specified methods 
        int TurnsToRefresh { get; }
        int TurnsUntilRefreshed { get; }


        bool Perform();
        void Tick();
    }
}
