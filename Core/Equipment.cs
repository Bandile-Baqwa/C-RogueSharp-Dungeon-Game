using RLNETConsoleGame.Interfaces;
using RLNETConsoleGame.Equipment;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Core
{
    public class Equipment  : IEquipment, ITreasure, IDrawable
    {
        public Equipment()
        {
            Symbol = ']';
            Color = RLColor.Yellow;
        }

       public string Name { get; set; }
       public int Awareness { get; set; }
       public int Attack { get; set; }
       public int AttackChance { get; set; }
       public int Defense { get; set; }
       public int DefenseChance { get; set; }
       public int Gold { get; set; }
       public int Health { get; set; }
       public int MaxHealth { get; set; }
       public int Speed { get; set; }
        public int Armor { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        protected bool Equals(Equipment other)
        {
            return Attack == other.Attack && AttackChance == other.AttackChance && Awareness == other.Awareness && Defense == other.Defense && DefenseChance == other.DefenseChance && Gold == other.Gold && Health == other.Health && MaxHealth == other.MaxHealth && string.Equals(Name, other.Name) && Speed == other.Speed;
        }

        public override bool Equals(object obj)     //this overrides the default Equals method inherited from the the base object class
        {   //it takes an object (of any type) as an argument 
            if (ReferenceEquals(null, obj))         //it first checkes if the provided object is null ,if it is it returns false
            {
                return false;
            }
            if (ReferenceEquals(this, obj)) //it checks if the provided object is the same ref as the current object then return true if it is
            {
                return true;
            }
            if (obj.GetType() != this.GetType())    //this verifies if the provided object is of the same type as the current Equipment object
            {
                return false;
            }
            return Equals((Equipment)obj);//this calls the custom equals method to compare the properties of the 2 objects
        }

        public override int GetHashCode()   //this overrides the default GetHashCode from the base object class
        {
            unchecked       //this key word makes sure that overflow exceptions arent thrown during calculations
            {
                var hashCode = Attack;
                hashCode = (hashCode * 397) ^ AttackChance;     //this calulates the HashCode fot the equipment object
                hashCode = (hashCode * 397) ^ Awareness;
                hashCode = (hashCode * 397) ^ Defense;
                hashCode = (hashCode * 397) ^ DefenseChance;
                hashCode = (hashCode * 397) ^ Gold;
                hashCode = (hashCode * 397) ^ Health;
                hashCode = (hashCode * 397) ^ MaxHealth;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Speed;
                hashCode = (hashCode * 397) ^ Armor;
                return hashCode;
            }
        }

        // (==) this is an overloaded equality operator for comparing two equipment objects
        public static bool operator ==(Equipment left, Equipment right)
        {
            return Equals(left, right); // the custom equals method is called to check if the objects are equal
        }
        public static bool operator !=(Equipment left, Equipment right)
        {
            return !Equals(left, right);        // same as above but it returns true if objects arent equal
        }

        public bool PickUp(IActor actor)
        {
            if (this is HeadEquipment)
            {
                actor.Head = this as HeadEquipment;
                Game.MessageLog.Add($"{actor.Name} picked up a {Name} Mask");
                return true;
            }
            if (this is BodyEquipment)
            {
                actor.Body = this as BodyEquipment;
                Game.MessageLog.Add($"{actor.Name} picked up {Name} body armor");
                return true;
            }
            if (this is HandEquipment)
            {
                actor.Hand = this as HandEquipment;
                Game.MessageLog.Add($"{actor.Name} picked up {Name}");
                return true;
            }
            if (this is FeetEquipment)
            {
                actor.Feet = this as FeetEquipment;
                Game.MessageLog.Add($"{actor.Name} picked up {Name}");
                return true;
            }
            return false;
        }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }
            if (map.IsInFov(X, Y))
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
