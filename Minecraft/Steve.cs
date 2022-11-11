using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Steve
    {
        public int Heart { get; private set; }
        public HashSet<Block> BlockInventory;
        //public List<Monster> LootInventory;
        public HashSet<Loot> LootInventory;
        public List<Item> Items;
        public int Sticks { get; private set; }
        public string Pickaxe { get; private set; }
        public string Sword { get; private set; }
        public Random Random { get; private set; }
        public Furnace Furnace { get; private set; }

        public Steve(int heart, int sticks, string pickaxe, string sword)
        {
            Heart = heart;
            Sticks = sticks;
            Pickaxe = pickaxe;
            Sword = sword;
            BlockInventory = new HashSet<Block>();
            Items = new List<Item>();
            //LootInventory = new List<Monster>();
            LootInventory = new HashSet<Loot>();
            Furnace = new Furnace(this);
            Random = new Random();
        }

        public void PickUpBlocks(HashSet<Block> blocks)
        {
            GetInInventory(blocks);
            
        }

        public void PickUpLoot(List<Loot> monsters)
        {
            GetInInventory(monsters);
        }

        public void GetInInventory(HashSet<Block> blocks)
        {
            BlockInventory.UnionWith(blocks);
        }

        public void GetInInventory(List<Loot> monsters)
        {
            LootInventory.UnionWith(monsters);
        }
        public void PrintBlocks()
        {
            foreach (var block in BlockInventory)
            {
                Console.WriteLine($"{block.Name} {block.Quantity}");
            }
        }

        public void PrintLoot()
        {
            foreach (var loot in LootInventory)
            {
                Console.WriteLine($"{loot.Name} {loot.Quantity}"); 
            }
        }

        public void ViewCraftingtable()
        {
            Console.WriteLine($"Dette er Furnace!!!" +
                              $"1: melt");
            var command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    Furnace.MeltIron();
                    break;
            }
        }

        public bool CheckForEnoughObsidian()
        {
            var obsidianToFind = BlockInventory.FirstOrDefault(x => x.Name == "obsidian");
            if (obsidianToFind != null && obsidianToFind.Quantity >= 11)
            {
                obsidianToFind.Quantity -= 11;
                Console.WriteLine($"Du har nå bygget en portal til nether av obsidian!");
                return true;
            }

            else
            {
                Console.WriteLine($"Du har ikke nok obsidian");
                return false;
            }
            
        }
            

        public void MakeFlint()
        {
            var name = "Flint and steel";
            var quantity = 1;
            var flintBlock = new Item(name, quantity);
            Items.Add(flintBlock);
        }

        public void CheckForEnoughGravel()
        {
            var flintToPrint = BlockInventory.FirstOrDefault(x => x.Name == "flint");
            if (flintToPrint.Name != null && flintToPrint.Quantity >= 1)
            {
                flintToPrint.Quantity -= 1;
                MakeFlint();
            }
            //foreach (var block in BlockInventory.ToList())
            //{
            //    if (block.Name == "gravel")
            //    {
            //        block.Quantity -= 1;
            //        MakeFlint();
            //    }
            //}
        }

        public void PrintItems()
        {
            foreach (var item in Items)
            {
                Console.WriteLine($"{item.Name} antall: {item.Quantity}");
            }
        }
        public void MineGravelToFlint()
        {
            var randomHitChance = Random.Next(1, 3);
            if (randomHitChance == 2)
            {
                Console.WriteLine($"Du fikk flint");
                CheckForEnoughGravel();
            }
            else
            {
                Console.WriteLine($"du fikk ikke flint ;(");
            }
        }

        public bool CheckForFlintAndSteel()
        {
            var blockToFind = BlockInventory.FirstOrDefault(x => x.Name == "iron");
            if (blockToFind != null && blockToFind.Quantity >= 1) {

                    Console.WriteLine($"Du har nok iron");
            }
            else
            {
                return false;
            }

            var flintToFind = BlockInventory.FirstOrDefault(x => x.Name == "flint");

            if (flintToFind != null && flintToFind.Quantity >= 1)
            {
                Console.WriteLine($"Du har nok flint");
            }
            else
            {
                return false;
            }
            return true;
        }
        

        public void CraftNetherPortal()
        {
            Console.WriteLine($"You have crafted the nether portal!!");
        }
       


        //public void MeltIron()
        //{
        //    if ()
        //    {
        //        foreach (var indexToEdit in BlockInventory)
        //        {

        //        }
        //    }
        //if (BlockInventory.N)
        //{

        //}
        //for (int i = 0; i < BlockInventory.Count; i++)
        //{
        //    if ()
        //    {

        //    }
        //if (BlockInventory.Contains(new Block("iron", 0)))
        //{
        //    BlockInventory = "steel";
        //}
        //}

    }

        //public bool Mine(int mineChance)
        //{
        //    return Random.Next(0, mineChance) == 2;
        //}

        //public void CraftNetherPortal()
        //{
            //går gjennom inventoryet og sjekker om du har obsidian og at du har nok!

        //}

    //    public void PrintBlocks()
    //    {
    //        foreach (var block in BlockInventory)
    //        {
    //            Console.WriteLine($"Blokker: {block.Name}");
    //        }

    //    }
    //    public void CraftEyeOfEnder()
    //    {
    //        //trenger blazepowder og ender pearl, 1 av begge
    //    }
    //}
}
