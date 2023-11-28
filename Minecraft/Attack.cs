using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Attack
    {
        public string AttackName { get; private set; }
        public int AttackDamage { get; private set; }

        public Attack(string attackName, int attackDamage)
        {
            AttackName = attackName;
            AttackDamage = attackDamage;
        }
    }
}
