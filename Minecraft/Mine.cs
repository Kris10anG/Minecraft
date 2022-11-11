using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Mine
    {
        public Steve Steve;
        public List<Block> Blocks;
        public List<Monster> Monsters;
        public int Quantity { get; private set; }
        public Random Random;
        public Mine(Steve steve)
        {
            Steve = steve;
            Blocks = new List<Block>
            {
                new Block("diamond", Quantity),
                new Block("gold", Quantity),
                new Block("iron ore", Quantity),
                new Block("stone", Quantity),
                new Block("lapiz", Quantity),
                new Block("gravel", Quantity),
                new Block("flint", Quantity),
                new Block("obsidian", Quantity),


            };
            Monsters = new List<Monster>
            {
                new Monster("Creeper", new List<Loot>{new Loot("Gunpovder", 0)}),
                new Monster("zombie", new List<Loot>{new Loot("Rotten flesh", 0)}),
                new Monster("spider", new List<Loot>{new Loot("Web", 0)}),
                new Monster("skeleton", new List<Loot>{new Loot("Bone", 0)}),
                new Monster("enderman", new List<Loot>{new Loot("Ender pearl", 0)}),

            };
            Random = new Random();
        }
        public void DropBlocks(HashSet<Block> blocks, Steve steve)
        {
            steve.PickUpBlocks(blocks);
        }

        public void DropLoot(List<Loot> loot, Steve steve)
        {
            steve.PickUpLoot(loot);
        }
        
        public void MineBlocks()
        {
            HashSet<Block> OresToPickup = new HashSet<Block>();
            for (int i = 0; i < Monsters.Count; i++) //Løkken sin størrelse er hvor mange blocks du vil mine
            {
                var index = Random.Next(0, Blocks.Count);
                var pickup = Blocks[index];
                pickup.Quantity++;
                //Block.DropBlocks(OresToPickup, steve);
                OresToPickup.Add(pickup);
            }

            foreach (var ore in OresToPickup) //løkker gjennom blokkene du har minet
            {
                Console.WriteLine($"Du minet: {ore.Name} og fikk {ore.Quantity}");

            }
            DropBlocks(OresToPickup, Steve);
        }

        public void FightMonsters()
        {
            List<Loot> MobsLoot = new List<Loot>(); // lager en tom liste av loot som heter MobsLoot
            var randomMobs = GetRandomMobs(); //lagrer det som varialbel for å bruke lista med random mobs
            if (randomMobs.Count == 0) //om det ikke er noen random mobs
            {
                Console.WriteLine($"Du møtte ingen monsters");
                return;
            }

            foreach (var monster in randomMobs) //løkker gjennom hver enkelte monster.
            {
                Console.WriteLine($" Du fant en: {monster.Name}");
                Console.WriteLine($"Ønsker du å drepe {monster.Name}? (ja/nei)\n");

                var command = Console.ReadLine();
                if (command == "ja")
                {
                    var droppedLoot = monster.DropLoot(); //lagrer lootet til monsteret som en variabel
                    foreach (var loot in droppedLoot)
                    {
                        Console.WriteLine($"Du fikk {loot.Quantity} {loot.Name}");
                    }

                    MobsLoot.AddRange(droppedLoot); // UnionWith er det samme som addrange
                }
               
            }
            DropLoot(MobsLoot, Steve);

            
        }

        public List<Monster> GetRandomMobs() //returtypen er List av Monster som vi returnerer
        {
            List<Monster> MonstersToGet = new List<Monster>(); //lager liste av monster som er tom
            for (int i = 0; i < 3; i++) //looper gjennom tre ganger
            {
                var randomIndex = Random.Next(0, Monsters.Count); //random int fra monsters sin lengde
                if (Random.Next(0, 2) == 1) //50% sjanse for at den treffer
                {
                    var chosenMonster = Monsters[randomIndex]; //lagrer de randome monsterne som vi får
                    MonstersToGet.Add(chosenMonster); //legger til de monsterne
                }
            }

            return MonstersToGet; //returnerer lista med monsterne
        }
       
        public void MineMode()
        {
            var run = true;
            
            while (run)
            {
                Console.WriteLine($"Hva ønsker du å gjøre i hula\n" +
                                  $"u: Mine blocks\n" +
                                  $"i: Drepe Monsters\n" +
                                  $"x: exit\n" +
                                  $"q: møte Thorbjørn og huleboerne?");
                var input = Console.ReadLine();
            switch (input)
            {
                case "u":
                    MineBlocks();
                    break;
                case "i":
                    FightMonsters();
                    break;
                case "x":
                    run = false;
                    break;
            }
            }

        }
    }
}
