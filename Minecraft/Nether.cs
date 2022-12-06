using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Nether
    {
        public Steve Steve;
        public List<Monster> BlazeSpawnerList;
        public List<Monster> NetherMonsters;
        public List<Item> ChestItems;
        public Random Random;
        public SqlDatabase Reader;
        public Nether(Steve steve, SqlDatabase reader)
        {
            Steve = steve;
            BlazeSpawnerList = new List<Monster>
            {
                new Monster("Blaze", 4,8,new List<Loot> {new Loot("Blaze rod", 1), },reader),
                new Monster("Blaze", 4,8,new List<Loot> {new Loot("Blaze rod", 1), },reader),
                new Monster("Blaze", 4,8,new List<Loot> {new Loot("Blaze rod", 1), },reader),

            };
            NetherMonsters = new List<Monster>();
            //{
            //    new Monster("Piglin", 2, new List<Loot> {new Loot("gold bar", 1)}),
            //    new Monster("Gast", 3, new List<Loot> {new Loot("gast tear", 1)}),

            //};
            ChestItems = new List<Item>
            {
                new Item("Golden apple", 0),
                new Item("Bow", 0),
                new Item("Arrow", 20),
                new Item("Name tag", 0),
            };
            Random = new Random();
            Reader = reader;
            GetMonsters();
        }

        public void EnemysAppeared()
        {
           var randomIndex = Random.Next(0, NetherMonsters.Count);
           var monster = NetherMonsters[randomIndex];
           Console.WriteLine($"Du møtte på {monster.Name}! Bank han opp");
           var hitInput = Console.ReadLine();
           if (hitInput == "q")
           {
               //if (!monster.Hearts >= 1)
               //{
               //    monster.
               //}
               Steve.HitMonsterWithSword(monster);
           }


        }
        public void SearchForNetherCastle()
        {
            var randomHit = Random.Next(1, 5);

            List<int> Directions = new List<int>
            {
                1, 2, 3, 4
            };
            bool searchForNetherCastle = true;
            while (searchForNetherCastle)
            {
                Console.WriteLine($"Velg retning du ønsker å gå:\n" +
                                  $"1: Nord\n" +
                                  $"2: Sør\n" +
                                  $"3: Øst\n" +
                                  $"4: Vest\n" +
                                  $"5: Gå tilbake til portalen");
                var directionInput = Console.ReadLine();
                var convertedDirectionInput = Convert.ToInt32(directionInput);
                if (convertedDirectionInput == 5)
                {
                    searchForNetherCastle = false;
                    return;
                }
                if (Directions.Contains(convertedDirectionInput))
                {
                    Directions.Remove(convertedDirectionInput);
                }
                else
                {
                    Console.WriteLine($"Velg en annen retning");
                }
                

                if (randomHit == convertedDirectionInput)
                {
                    Console.WriteLine($"Du fant slottet!");
                    NetherCastle();
                }
                else
                {
                    EnemysAppeared();
                    Console.WriteLine($"Velg en annen retning neste gang!");

                }
            }
        }
            

        public void NetherCastle()
        {
            var inNetherCastle = true;
            while (inNetherCastle)
            {
                
            
            Console.WriteLine($"Du leter i slottet og finner:\n" +
                              $"1: Kiste\n" +
                              $"2: blaze spawner\n" +
                              $"3: Wither skeletons\n" +
                              $"4: Gå ut av slottet\n");
            Console.WriteLine($"Hva vil du utforske først");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Console.WriteLine($"Du åpner kisten og finner:\n");
                    OpenChest();
                    break;
                case "2":
                    BlazeSpawnerData();
                    break;
                    case "3":
                        break;
                    case "4":
                        inNetherCastle = false;
                        break;
            }
            }

        }
        public List<Monster> BlazeSpawner()
        {
            Console.WriteLine($"Du går til spawneren...");
            List<Monster> Blaze = new List<Monster>();
            for (int i = 0; i < BlazeSpawnerList.Count; i++)
            {
                if (BlazeSpawnerList[i].Hearts > 0)
                {
                    Blaze.Add(BlazeSpawnerList[i]);
                }

                //var monster = BlazeSpawnerList[i];
                //Blaze.Add(monster);
            }
            return Blaze;
        }
        
        public void BlazeSpawnerData()
        {
            var avaliableBlazes = BlazeSpawner();
            for (int i = 0; i < avaliableBlazes.Count; i++)
            {
                FightBlaze(avaliableBlazes[i]);
                    Console.WriteLine($"Ønsker du å fortsette? ja/nei");
                    var input = Console.ReadLine();
                    if (input != "ja")
                    {
                        return;
                    }
            }

        }

        public void FightBlaze(Monster blaze)
        {
            Console.WriteLine($"Du møter: {blaze.Name}, den har {blaze.Hearts} hjerter");

            while (blaze.Hearts >= 1)
            {
                var fightMonsterInput = Console.ReadLine();
                if (fightMonsterInput == "q")
                {
                    Steve.HitMonsterWithSword(blaze);

                }
                if (blaze.Hearts >= 1)
                {
                    blaze.DamageSteve(Steve);
                }
                else
                {
                    Console.WriteLine($"Du drepte Blaze med {Steve.Sword}");
                    Console.WriteLine($"du fikk {blaze.Loot[index:0]}");
                    Steve.PickUpLoot(blaze.Loot);
                    
                }
            }
        }
        public async Task OpenChest()
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < ChestItems.Count; i++)
            { 
                var item = ChestItems[i];
            item.IncreaseLoot();
            Console.WriteLine($"Du fikk: {item.Name}, Antall: {item.Quantity}");
            items.Add(item);
            await Reader.AddNewItem(item);

            }
            //Steve.PickUpLoot(items);

        }
        public void DropBlazeRod()
        {

        }
        public async Task GetMonsters()
        {
            var monsters = await Reader.NetherMonsters();
            monsters.ForEach(x =>
            {
                NetherMonsters.Add(new Monster(x.Name, x.Hearts, x.Id, new List<Loot>(), Reader));
            });
        }
        public void NetherHandler()
        {
            var netherMode = true;
            while (netherMode)
            {
                Console.WriteLine($"Du er nå i: Nether\n" +
                                  $"1: Lete etter Nether castle\n" +
                                  $"x: Gå til vanlig verden");
                var choseOptions = Console.ReadLine();
                switch (choseOptions)
                {
                    case "1":
                        SearchForNetherCastle();
                        break;
                    case "x":
                        netherMode = false;
                        break; 
                }
            }
        }
    }
}
