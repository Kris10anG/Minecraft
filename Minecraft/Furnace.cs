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
        public SqlDatabase Reader;

        public Furnace(Steve steve, SqlDatabase reader)
        {
            Steve = steve;
            Reader = reader;
        }

        public async Task<Item> MeltOres()
        {
            Steve.PrintBlocks();
            Console.WriteLine($"Velg hvilken blokk du vil smelte");
            var ironInput = Console.ReadLine();
            var blocks = await Steve.GetStevesBlocks();

            var blockToMelt = blocks.FirstOrDefault(ironBlock => ironBlock.Name == ironInput);
            var ironBlock = "Iron ore";
            if (blockToMelt.Name == ironBlock)
            {
               await SmeltToIron();
            }
            else if (blockToMelt.Name == "gold ore")
            {
                blockToMelt.Name = "Gold";
                Console.WriteLine($"Du smeltet gold ore til {blockToMelt.Name}");
                await Reader.AddBlock(blockToMelt);

            }
            else
            {
                Console.WriteLine($"Steve fant ikke {blockToMelt.Name}");
            }

            return blockToMelt;
        }

        public async Task SmeltToIron()
        {
            var itemName = "Iron";
            var itemQuantity = 1;
            var item = new Item(itemName, itemQuantity);
            await Reader.RemoveSelectedBlock("Iron ore");
            await Reader.AddNewItem(item);
            Console.WriteLine($"Du smeltet iron ore til {item.Name}");
        }
    }
}
