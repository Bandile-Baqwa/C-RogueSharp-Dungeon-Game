using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNETConsoleGame.Abilities;
using RLNETConsoleGame.Behaviors;
using RLNETConsoleGame.Core;
using RLNETConsoleGame.Items;

namespace RLNETConsoleGame.Systems
{
    public static class AbilityGenerator
    {
        public static Pool<Ability> _abilityPool = null;        //this is a pool that contians all the abilities

        public static Ability CreateAbility()
        {
            if (_abilityPool == null)
            {
                _abilityPool = new Pool<Ability>();
                _abilityPool.Add(new Heal(10), 10);
                _abilityPool.Add(new Missile(2, 80), 10);
                _abilityPool.Add(new RevealMap(15), 10);
                _abilityPool.Add(new Whirlwind(), 10);
                _abilityPool.Add(new MolotovCocktail(4, 60, 2), 10);
                _abilityPool.Add(new SuperStatic(6, 40), 10);

            }

            return _abilityPool.Get();
        }
    }
}
