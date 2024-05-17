using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.Random;

namespace RLNETConsoleGame.Systems
{
    public class Pool<T>        //this is a pool of type T 
    {
        private readonly List<PoolItem<T>> _poolItems;      //this holds all the items in the pool
        private static readonly IRandom _random = new DotNetRandom();       //random number generator
        private int _totalWeight;   // each item in the pool will be assigned a weight to determin the properbility of the item being selected

        public Pool()   // this instantiates the _poolItems list
        {
            _poolItems = new List<PoolItem<T>>();
        }

        public T Get()      //this selects an item from the pool based on its weight 
        {
            int runningWeight = 0;
            int roll = _random.Next(1, _totalWeight);   //this generates a random number between 1 and the total weight of all the items in the pool 
            foreach (var poolItem in _poolItems)
            {
                runningWeight += poolItem.Weight;   //each items weight is added to the running weight until the mumber is <= the random number
                if (roll <= runningWeight)
                {
                    Remove(poolItem);       //if the weight is greated then the item is removed from the pool and returned
                    return poolItem.Item;
                }
            }
            throw new InvalidOperationException("Could not get an item from the pool");
        }

        public void Add(T item, int weight)     //adds item to pool with weight
        {
            _poolItems.Add(new PoolItem<T> { Item = item, Weight = weight });
            _totalWeight += weight;
        }

        public void Remove(PoolItem<T> poolItem)    //removes item from pool 
        {
            _poolItems.Remove(poolItem);
            _totalWeight -= poolItem.Weight;
        }
    }

    public class PoolItem<T>
    {
        public int Weight { get; set; }
        public T Item { get; set; }
    }
}
