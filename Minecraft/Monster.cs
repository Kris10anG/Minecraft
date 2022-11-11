using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Monster
    {
        public string Name { get; private set; }
        public List<Loot> Loot { get; private set; }
        //public string Loot { get; private set; }

        public Monster(string name, List<Loot> loot)
        {
            Name = name;
            Loot = loot;
        }

        public HashSet<Loot> DropLoot()
        {
            var random = new Random(); //random
            var randomQuantity = random.Next(1, 4); //tilfeldig antall fra 1-3
            HashSet<Loot> DroppedLoot = new HashSet<Loot>(); //lager en tom liste av loot
            for (int i = 0; i < randomQuantity; i++) //løkker gjennom det tilfeldige tallet, ta
            {
                var randomLootIndex = random.Next(0, Loot.Count); //lagrer et tilfeldig nummer mellom lengden på lista i loot
                var loot = Loot[randomLootIndex];
                loot.IncreaseLoot();
                DroppedLoot.Add(loot); //legger til det randome lootet i droppedLoot metoden
            }

            return DroppedLoot; //metoden returnerer lista med loot
        }
    }
}
