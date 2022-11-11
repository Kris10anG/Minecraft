﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Block
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Block(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

       
    }
}
