using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Core
{
    public class Help
    {
        readonly List<string> helpList = new List<string>(new string[]
        {
            "H - Help",
            "Q , W ,E ,R - Abilty buttons",
            "1, 2, 3 ,4 - Item buttons",
            "S - Store",
            "Period (.) - Move up stairs",
            "Arrows - Move player",
            "Escape - Close game"

        });



        // Display the available items in the store
        public void DisplayhelpList()
        {
            Game.MessageLog.Add("Hey heres a list of all the buttons and what they do");
            foreach (string list in helpList)
            {
                Game.MessageLog.Add($"{list}");
            }
        }
    }
}
