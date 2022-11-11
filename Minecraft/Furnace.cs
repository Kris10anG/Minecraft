using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Furnace
    {
        public Steve Steve;

        public Furnace(Steve steve)
        {
            Steve = steve;
        }

        //public Block Melt()
        //{
        //    Console.WriteLine($"Hva har du lyst til å smelte?");
        //    Steve.PrintBlocks();
        //    var blockInput = Console.ReadLine();
        //    Block? blockToSmelt = Steve.BlockInventory.FirstOrDefault(block => block?.Name == blockInput);
        //    if (blockToSmelt == null)
        //    {
        //        Console.WriteLine($"er ikke noe å smelte");
        //        return null;
        //    }

        //    if (blockToSmelt is Block)
        //    {
        //        blockToSmelt = (Block)blockToSmelt;
        //        blockToSmelt.Name = "iron ingot";
        //    }

        //    return blockToSmelt;
        //}

        public Block MeltIron()
        {
            Steve.PrintBlocks();
            Console.WriteLine($"Velg hvilken blokk du vil smelte");
            var ironInput = Console.ReadLine();
            var blockToMelt = Steve.BlockInventory.FirstOrDefault(ironBlock => ironBlock.Name == ironInput);
            if (blockToMelt.Name == "iron ore")
            {
                blockToMelt.Name = "iron";
                Console.WriteLine($"Du smeltet iron ore til {blockToMelt.Name}");
            }
            else if (blockToMelt.Name == "gold ore")
            {
                blockToMelt.Name = "gold";
                Console.WriteLine($"Du smeltet gold ore til {blockToMelt.Name}");

            }
            else
            {
                Console.WriteLine($"Steve fant ikke {blockToMelt.Name}");
            }

            return blockToMelt;
        }
    }
}
