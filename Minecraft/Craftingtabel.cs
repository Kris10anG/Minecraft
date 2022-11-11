using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Craftingtabel
    {
        public List<Item> Items { get; private set; }

        public Craftingtabel(List<Item> items)
        {
            Items = items;
        }
        
    }
}
