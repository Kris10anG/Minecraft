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
        private SqlDatabase _reader;
       
        public List<Monster> Monsters;
        public int Quantity { get; private set; }
        public Random Random;
        public Mine(Steve steve, SqlDatabase reader)
        {
            _reader = reader;
            Monsters = new List<Monster>();
            GetStartData();
            Steve = steve;
            Random = new Random();
        }

       

        public async Task GetStartData()
        {
            var monsters = await _reader.GetMonsters();
            monsters.ForEach(x=>
            {
                Monsters.Add(new Monster(x.Name, x.Hearts,x.Id, new List<Loot>(), _reader));
            });
           await Commen.InitiateList(_reader);
        }
        public void DropBlocks(HashSet<Block> blocks) //sende data videre fra en metode til en annen
        {
            Steve.PickUpBlocks(blocks);
        }

        public void DropLoot(List<Loot> loot)
        {
            Steve.PickUpLoot(loot);
        }

      
        public async Task MineBlocks()
        {
            HashSet<Block> OresToPickup = new HashSet<Block>();
            for (int i = 0; i < 6; i++) //Løkken sin størrelse er hvor mange blocks du vil mine
            {
                var index = Random.Next(0, Commen.Blocks.Count);
                var pickup = Commen.Blocks[index];
                pickup.IncreaseLoot();
                //Block.DropBlocks(OresToPickup, steve);
                OresToPickup.Add(pickup);
            }
            await _reader.AddBlocks(OresToPickup);

            foreach (var ore in OresToPickup) //løkker gjennom blokkene du har minet
            {
                Console.WriteLine($"Du minet: {ore.Name} og har nå {ore.Quantity} {ore.Name}");

            }
            DropBlocks(OresToPickup);

        }

        public async Task FightMonsters()
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
                        await _reader.AddNewItem(loot);
                        Console.WriteLine($"Du fikk {loot.Quantity} {loot.Name}");
                    }

                    MobsLoot.AddRange(droppedLoot); // UnionWith er det samme som addrange
                }
               
            }
            DropLoot(MobsLoot);

            
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
       
        public async Task MineMode()
        {
            var run = true;
            
            while (run)
            {
                Console.WriteLine($"Hva ønsker du å gjøre i hula\n" +
                                  $"u: Mine blocks\n" +
                                  $"i: Drepe Monsters\n" +
                                  $"x: exit\n");
                var input = Console.ReadLine();
            switch (input)
            {
                case "u":
                   await MineBlocks();
                    break;
                case "i":
                   await FightMonsters();
                    break;
                case "x":
                    run = false;
                    break;
            }
            }

        }
    }
}
