﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Minecraft
{
    internal class Steve
    {
        public int Hearts { get; private set; }
        public HashSet<Loot> LootInventory;
        public HashSet<Item> Items;
        public int Sticks { get; private set; }
        public string Pickaxe { get; private set; }
        public string Sword { get; private set; }
        public Random Random { get; private set; }
        public Furnace Furnace { get; private set; }
        public SqlDatabase Reader;

        public Steve(int hearts, int sticks, string pickaxe, string sword, SqlDatabase reader)
        {
            Hearts = hearts;
            Sticks = sticks;
            Pickaxe = pickaxe;
            Sword = sword;
            Items = new HashSet<Item>();
            LootInventory = new HashSet<Loot>();
            Furnace = new Furnace(this, reader);
            Random = new Random();
            Reader = reader;

        }

        public void NoHearts(string chosenChar)
        {
            if (Hearts <= 0)
            {
                Console.WriteLine($"You died ;(\n" +
                                  $"Press {chosenChar} to start over!");
                var userInput = Console.ReadLine();
                if (userInput == chosenChar)
                {
                    Environment.Exit(0);
                }
            }
        }
        public async Task PickUpBlocks(HashSet<Block> blocks)
        {
           await GetInInventory(blocks);
            
        }
        public void PickUpLoot(List<Loot> monsters)
        {
            GetInInventory(monsters);
        }
        public async Task GetInInventory(HashSet<Block> blocks)
        {
           await Reader.AddBlocks(blocks);
        }
        public void GetInInventory(List<Loot> monsters)
        {
            LootInventory.UnionWith(monsters);
        }
        public async Task< HashSet<Block>> GetStevesBlocks()
        {
            var minedBlocks = await Reader.StevesBlocks();
            var blocks = SortBlocks(minedBlocks);
            return blocks;
        }
        public async Task PrintBlocks()
        {
            var blocks = await GetStevesBlocks();
            foreach (var block in blocks)
            {
                Console.WriteLine($"block: {block.Name} Antall: {block.Quantity}");
            }
        }
        public HashSet<Block> SortBlocks(HashSet<Block> blocks)
        {
            HashSet<Block> OresToPickup = new HashSet<Block>();
            foreach (var block in Commen.Blocks) //alle blokkene i spillet
            {
                var minedBlocks = blocks.Where(x => x.Name == block.Name); //ser de jeg har minet, finner alle elementene som heter det samme
                var quantity = minedBlocks.Sum(mined => mined.Quantity); //bruker forrige variabel ti
                OresToPickup.Add(new Block(block.Name, quantity)); //tar hver enkelt blokk og legger sammen alle quantitetene
            }
            return OresToPickup;
        }

        public async Task<HashSet<Item>> SortItems()
        {
            HashSet<Item> ItemList = new HashSet<Item>();
            List<string> NameList = new List<string>();

            var lootInventory = await Reader.GetItemData();
            foreach (var loot in lootInventory)
            {
                if(!NameList.Contains(loot.Name))
                {
                    NameList.Add(loot.Name);

                }
            }
            foreach (var name in NameList) //alle blokkene i spillet
            {
                var items = lootInventory.Where(x => x.Name == name); //ser de jeg har minet, finner alle elementene som heter det samme
                var quantity = items.Sum(item => item.Quantity);
                ItemList.Add(new Item(name, quantity));
            }
            return ItemList;
        }
        public void PrintLoot()
        {
            foreach (var loot in LootInventory)
            {
                Console.WriteLine($"{loot.Name} {loot.Quantity}"); 
            }
        }
        public async Task PrintItems()
        {
            var items = await SortItems();
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name} {item.Quantity}");
            }
        }

        //public async Task<bool> CheckForBlazeRod()
        //{
        //    var items = SortItems();

        //    foreach (var item in await items)
        //    {
        //        if (item.Name == "Blaze powder" && item.Quantity >= 1)
        //        {
        //            item.Use();
        //            Console.WriteLine($"Du har nok blaze powder til å lage");
        //            return true;
        //        }
        //    }
        //    return false;

        //}

        public async Task<bool> CheckItem(string itemName)
        {
            var items = SortItems();
            foreach (var item in await items)
            {
                if (item.Name == itemName && item.Quantity >= 1)
                {
                    //await Reader.RemoveItem(itemName, 1);
                    Console.WriteLine($"Du har nok av {item.Name} til å lage Eye of ender");
                    return true;
                }
            }

            return false;
        }
        //public async Task<bool> CheckEnderPearl()
        //{
        //    var items = SortItems();
        //    foreach (var item in await items)
        //    {
        //        if (item.Name == "Enderpearl" && item.Quantity >= 1)
        //        {
        //            item.Use();
        //            Console.WriteLine($"Du har nok av {item.Name} til å lage Eye of ender");
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //public async Task CreateEyeOfEnder()
        //{
        //    var monsterLoot = await Reader.GetMonsterLoot("Enderpearl");
        //    for (int i = 0; i < monsterLoot.Count; i++)
        //    {
        //        if (monsterLoot[i] is {Name: "Enderpearl"} )
        //        {
        //            if (monsterLoot[i].Quantity >= 1)
        //            {
        //                var enderPearl = new Item("Eye of ender", 1);
        //                await Reader.AddNewItem(enderPearl);
        //            }
        //        }
        //    }
        //}

        public async Task CreateEyeOfEnder()
        {
            var eyeOfEnder = new Item("Eye of ender", 1);
            await Reader.AddNewItem(eyeOfEnder);
        }
        public async Task ConvertBlazeRod()
        {
            var itemName = LootInventory.FirstOrDefault(blazeRod => blazeRod.Name == "Blaze rod");
            if (itemName is {Name: "Blaze rod"})
            {
                LootInventory.Remove(itemName);
                var item = new Item("Blaze powder", 2);
                await Reader.AddNewItem(item);
                Console.WriteLine($"Du har nå laget {item.Name} og har {item.Quantity} stykker");
            }
        }
        public async Task UseItem()
        {
            PrintItems();
            var items = await Reader.GetItemData();
            Console.WriteLine($"Hvilket item ønsker du å bruke?");
            var itemToUse = Console.ReadLine();
          var item =  items.FirstOrDefault(x => x.Name == itemToUse);
            if (item == null) return;
            switch (itemToUse)
            {
                case "Golden apple":
                    var heartsIncrease = 5;
                    Hearts += heartsIncrease;
                    Console.WriteLine($"Du økte hjertene med {heartsIncrease} og har nå {Hearts}");
                    await Reader.RemoveSelectedBlock("Golden apple", 1);
                    break;
                case "Bow":

                    break;
            }
        }
        public void LoseHearts()
        {
            Hearts -= 3;
            Console.WriteLine($"Steve lost 3 hearts and now has {Hearts} hearts left!");
        }

        public void AttackEnderdragon(Enderdragon enderdragon)
        {
            enderdragon.TakeDamageFromSteve(this);
            Console.WriteLine($"You attacked {enderdragon.Name}! It has {enderdragon.Hearts} left");
        }

        public void TakeDamageFromEnderdragon(Enderdragon enderdragon)
        {
            var enemyAttack = enderdragon.AttackSteve();
            foreach (var attack in enemyAttack)
            {
                Hearts -= attack.AttackDamage;
                Console.WriteLine($"{enderdragon.Name} used {attack.AttackName} and tok {attack.AttackDamage} hearts from steve");
            }
            Console.WriteLine($"Steve har {Hearts} left");
        }
       
        public async Task Melt()
        {
            await Furnace.MeltOres();
        }

        public async Task<bool> CheckQuantity(string itemName, int quantity, Task<List<ItemData>> getItem )
        {
            var items = await Reader.GetItemData();
            var itemToFind = items.FirstOrDefault(item => item.Name == itemName);

            if (itemToFind != null && itemToFind.Quantity >= quantity)
            {
                Console.WriteLine($"Du har nok av {itemToFind.Name}");
                return true;
            }
            return false;
        }

        public async Task<bool> CheckQuantity(string itemName, int quantity, Task<HashSet<Block>> getItem)
        {
            var steveBlocks = await GetStevesBlocks();
            var itemToFind = steveBlocks.FirstOrDefault(item => item.Name == itemName);
            if (itemToFind != null && itemToFind.Quantity >= quantity)
            {
                        Console.WriteLine($"Du har nok av {itemToFind.Name}");
                        return true;
            }

            return false;
        }
        //refaktorerte denne metoden med CheckQuanity metoden for å kunne velge hvilken blokk jeg vil sjekke
        //public async Task<bool> CheckForEnoughObsidian()
        //{
        //    var blocks = await GetStevesBlocks();
        //    var obsidianToFind = blocks.FirstOrDefault(x => x.Name == "Obsidian");
        //    if (obsidianToFind != null && obsidianToFind.Quantity >= 10)
        //    {
        //        obsidianToFind.Quantity -= 10;
        //        Console.WriteLine($"Du har nå bygget en portal til nether av obsidian!");
        //        return true;
        //    }

        //    else
        //    {
        //        Console.WriteLine($"Du har ikke nok obsidian");
        //        return false;
        //    }
        //}

        public void HitMonsterWithSword(Monster monster)
        {
            monster.LoseHearts();
            Console.WriteLine($"You hit {monster.Name} with {Sword}! {monster.Name} has {monster.Hearts} hearts left");
            
        }

        public async Task MakeFlint()
        {

            var name = "Flint and steel";
            var quantity = 1;
            var flintBlock = new Item(name, quantity);
           await Reader.AddNewItem(flintBlock);
        }

        //public async Task CheckForEnoughGravel()
        //{
        //    var items = await Reader.GetItemData();
        //    var item = CheckQuantity("Flint", 1, items,);

        //    //var flintToPrint = blocks.FirstOrDefault(x => x.Name == "Gravel");

        //    //if (flintToPrint.Name != null && flintToPrint.Quantity >= 1)
        //    //{
        //    //    var userInput = Console.ReadLine();
        //    //    await Reader.RemoveSelectedBlock(userInput, 1);
        //    //    var flintItem = new Item("Flint", 1);
        //    //    await Reader.AddNewItem(flintItem);
        //    //}

        //}

        public async Task GetItem(string itemName, int itemQuantity)
        {
            var item = new Item(itemName, itemQuantity);
           await Reader.AddNewItem(item);
        }
        public async Task MineGravelToFlint()
        {
            var randomHitChance = Random.Next(1, 3);
            if (randomHitChance == 2)
            {
                Console.WriteLine($"Du fikk flint");
                await GetItem("Flint", 1);
                await Reader.RemoveSelectedBlock("Gravel", 1);


            }
            else
            {
                Console.WriteLine($"du fikk ikke flint ;(");
            }
        }

        public async Task<bool> CheckForFlintAndSteel()
        {
          var items = await Reader.GetItemData();
          var itemToFind =items.FirstOrDefault(x => x.Name == "Flint and steel");
            if (itemToFind != null)
            {
                return true;
            }
            var blocks = await GetStevesBlocks();
            var blockToFind = blocks.FirstOrDefault(x => x.Name == "Iron");
            if (blockToFind != null) {

                    Console.WriteLine($"Du har nok iron");
            }
            else
            {
                return false;
            }

            var flintToFind = blocks.FirstOrDefault(x => x.Name == "Flint");
            //
            var blockList = Reader.StevesBlocks();
            var flint = CheckQuantity("Flint", 1, blockList);

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


        public async Task SteveHandler()
        {
            Console.WriteLine($"Hva ønsker du sjekke?\n" +
                              $"1: BlockInventory\n" +
                              $"2: Monsters loot inventory\n" +
                              $"3: Sjekke om du kan lage tenner\n" +
                              $"4: Item list\n" +
                              $"5: Bruke Items\n" +
                              $"6: lag blazepowder\n" +
                              $"7: lag Eye of ender");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                   await PrintBlocks();
                    break;
                case "2":
                    PrintLoot();
                    break;
                case "3":
                   await CheckForFlintAndSteel();
                    break;
                case "4":
                   await PrintItems();
                    break;
                case "5":
                   await UseItem();
                    break;
                case "6":
                     await ConvertBlazeRod();
                    break;
                case "7":
                    //var enderPearl = CheckEnderPearl();
                    //var blazeRod = CheckForBlazeRod();
                    if (await CheckItem("Enderpearl")
                    && await CheckItem("Blaze powder"))
                    {
                       await CreateEyeOfEnder();
                    }
                    break;
                case "8":
                    MineGravelToFlint();
                    break;
            }
        }


        //public void MeltOres()
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
