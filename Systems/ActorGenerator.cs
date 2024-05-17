using RLNETConsoleGame.Core;
using RLNETConsoleGame.Monsters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RLNETConsoleGame.Systems
{
    public static class ActorGenerator
    {
        private static Player _player = null;

        public static Monster CreateMonster(int mapLevel, Point location)
        {
            Pool<Monster> monsterPool = new Pool<Monster>();
            monsterPool.Add(Roadman.Create(mapLevel), 25);
            monsterPool.Add(Op.Create(mapLevel), 25);
            monsterPool.Add(Mercenary.Create(mapLevel), 50);

            Monster monster = monsterPool.Get();
            monster.X = location.X;
            monster.Y = location.Y;

            return monster;
        }

        public static Player CreatePlayer()
        {
            if (_player == null)
            {
                _player = new Player()
                {
                Attack = 2,
                AttackChance = 50,
                Defense = 2,
                DefenseChance = 50,
                Gold = 0,
                Health = 100,
                MaxHealth = 100,
                Speed = 10,
                Awareness = 15,
                Name = "Agent Bands",
                Color = Colors.Player,
                Symbol = '@'
            };
            }
            return _player;
        }
    }
}
