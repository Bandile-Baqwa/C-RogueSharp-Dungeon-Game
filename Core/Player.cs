using RLNET;
using RLNETConsoleGame.Interfaces;
using RLNETConsoleGame.Abilities;
using RLNETConsoleGame.Equipment;
using RLNETConsoleGame.Items;
using RLNETConsoleGame.Monsters;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Core
{
    public class Player : Actor ,IActor
    {

        public IAbility QAbility { get; set; }
        public IAbility WAbility { get; set; }
        public IAbility EAbility { get; set; }
        public IAbility RAbility { get; set; }

        public IItem Item1 { get; set; }
        public IItem Item2 { get; set; }
        public IItem Item3 { get; set; }
        public IItem Item4 { get; set; }

        public Player()                 // of these properties are aval because it inherited from class Actor
        {
            QAbility = new DoNothing();
            WAbility = new DoNothing();
            EAbility = new DoNothing();
            RAbility = new DoNothing();

            NoItem noItem = new NoItem();
            Item1 = (IItem)noItem;
            Item2 = (IItem)noItem;
            Item3 = (IItem)noItem;
            Item4 = (IItem)noItem;


            Attack = 2;
            AttackChance = 50;
            Defense = 2;
            DefenseChance = 50;
            Gold = 0;
            Health = 100;
            MaxHealth = 100;
            Speed = 10;
            Awareness = 15;
            Name = "Agent Bands";
            Color = Colors.Player;
            Symbol = '@';
        }

        public bool AddAbility(IAbility ability)
        {
            if (QAbility is DoNothing)      //the is operator will check if the runtime type of QAbility is the same as DoNothing and return a boolean result(true or false)
            {
                QAbility = ability;
            }
            else if (WAbility is DoNothing)
            {
                WAbility = ability;
            }
            else if (EAbility is DoNothing)
            {
                EAbility = ability;
            }
            else if (RAbility is DoNothing)
            {
                RAbility = ability;
            }
            else
            {
                return false;
            }

            return true;    // this return ttuer is there to show the method was successful and added a new abilty 
        }

        public bool AddItem(IItem item)
        {
            if (Item1 is NoItem)
            {
                Item1 = item;
            }
            else if (Item2 is NoItem)
            {
                Item2 = item; 
            }
            else if (Item3 is NoItem)
            {
                Item3 = item;
            }
            else if (Item4 is NoItem)
            {
                Item4 = item;
            }
            else
            {
                return false;
            }

            return true;
        }

        //public void AddMonsterGoldToPlayer()
        //{
        //    var monsterGold = new Roadman();
        //    var playerGold = new Player();
        //    playerGold.PlayerGold += monsterGold.MonsterGold;
        //}

        public void DrawStats(RLConsole statConsole)
        {
           statConsole.Print(1, 1, $"Name :  {Name}", Colors.Text);
           statConsole.Print(1, 3, $"Health :  {Health}/{MaxHealth}", Colors.Text);
           statConsole.Print(1, 5, $"Attack :  {Attack}/{AttackChance}", Colors.Text);
           statConsole.Print(1, 7, $"Defense :  {Defense}/{DefenseChance}", Colors.Text);
           statConsole.Print(1, 9, $"Gold :  {Gold}", Colors.Gold);
        }

        public void DrawInvemtory(RLConsole inventoryConsole)
        {
            inventoryConsole.Print(1, 1, "Equipment", Colors.InventoryHeading);
            inventoryConsole.Print(1, 3, $"Head :{Head.Name}", Head == HeadEquipment.None() ? Palatte.DbOldStone : Palatte.DbLight);
            inventoryConsole.Print(1, 5, $"Body :{Body.Name}", Body == BodyEquipment.None() ? Palatte.DbOldStone : Palatte.DbLight);
            inventoryConsole.Print(1, 7, $"Hand:{Hand.Name}", Hand == HandEquipment.None() ? Palatte.DbOldStone : Palatte.DbLight);
            inventoryConsole.Print(1, 7, $"Feet:{Feet.Name}", Feet == FeetEquipment.None() ? Palatte.DbOldStone : Palatte.DbLight);

            inventoryConsole.Print(28, 1, "Abilities", Colors.InventoryHeading);
            DrawAbility(QAbility, inventoryConsole, 0);
            DrawAbility(WAbility, inventoryConsole, 1);
            DrawAbility(EAbility, inventoryConsole, 2);
            DrawAbility(RAbility, inventoryConsole, 3);

            inventoryConsole.Print(55, 1, "Items", Colors.InventoryHeading);
            DrawItem(Item1, inventoryConsole, 0);
            DrawItem(Item2, inventoryConsole, 1);
            DrawItem(Item3, inventoryConsole, 2);
            DrawItem(Item4, inventoryConsole, 3);
        }

        private void DrawAbility(IAbility ability, RLConsole inventoryConsole, int position)
        {
            char letter = 'Q';
            if (position == 0)
            {
                letter = 'Q';
            }
            else if (position == 1)
            {
                letter = 'W';
            }
            else if (position == 2)
            {
                letter = 'E';
            }
            else if (position == 3)
            {
                letter = 'E';
            }

            RLColor highLightTextColor = Palatte.DbOldStone;
            if (!(ability is DoNothing))
            {
                if (ability.TurnsUntilRefreshed == 0)
                {
                    highLightTextColor = Palatte.DbLight;
                }
                else
                {
                    highLightTextColor = Palatte.DbSkin;
                }
            }

            int xPosition = 28;
            int xHeighLightPosition = 28 + 4;

            int yPosition = 3 + (position * 2);
            inventoryConsole.Print(xPosition, yPosition, $"{letter} - {ability.Name}", highLightTextColor);

            if (ability.TurnsToRefresh > 0)
            {
                // this will calculate the width based on the ratio of the ability.turnsuntil refeshed 
                int width = Convert.ToInt32(((double)ability.TurnsUntilRefreshed / (double)ability.TurnsToRefresh) * 16.0); //this is the formula used in brackes and result is converted to int 32
                int remainingWidth = 20 - width;
                //this sets the background color based on the progress of the ability
                inventoryConsole.SetBackColor(xHeighLightPosition, yPosition, width, 1, Palatte.DbOldBlood);
                inventoryConsole.SetBackColor(xHeighLightPosition + width, yPosition, remainingWidth, 1, RLColor.Black);
            }
        }

        private void DrawItem(IItem item, RLConsole inventoryConsole, int position)
        {
            int xPosition = 55;
            int yPosition = 3 + (position * 2);
            string place = (position + 1).ToString();
                                                                                 // this is an "if" statement , if item is of type NoItem it will be dbOldStone if not it will be DbLight
            inventoryConsole.Print(xPosition, yPosition, $"{place} - {item.Name}", item is NoItem ? Palatte.DbOldStone : Palatte.DbLight);
        }

        public void Tick()
        {
            QAbility?.Tick();       //? is used for null conditional access
            WAbility?.Tick();       // is XAbility has an object then tick() is called if it is null then it skips it & doesn throw an exception 
            EAbility?.Tick();
            RAbility?.Tick();
        }
    }
}
