using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Loot
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }

        public Loot(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public void IncreaseLoot()
        {
            Quantity++;
        }

    }
}
