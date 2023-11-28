using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Enderdragon
    {
        public string Name { get; private set; }
        public int Hearts { get; private set; }
        public List<Attack> Attacks;
        public int Damage { get; private set; }
        public int Xp { get; private set; }
        public Random Random;
        public Enderdragon(string name, int hearts, int damage, int xp)
        {
            Name = name;
            Hearts = hearts;
            Damage = damage;
            Xp = xp;
            Attacks = new List<Attack>
            {
                new Attack("Dragon Breath", 1),
                new Attack("Fire Ball", 3),
            };
            Random = new Random();
        }

        public void TakeDamageFromSteve(Steve steve)
        {
            Hearts -= 3;
        }
        public List<Attack> AttackSteve()
        {
            List<Attack> Attack = new List<Attack>();
            var randomAttack = Random.Next(Attacks.Count);
            for (int i = 0; i < 1; i++)
            {
                var chosenAttack = Attacks[randomAttack];
                Attack.Add(chosenAttack);

            }
            return Attack;
        }

    }
}
