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
        public int Hearts { get; private set; }
        public List<Loot> Loot { get; private set; }

        private SqlDatabase _reader;
        //public string Loot { get; private set; }

        public Monster(string name, int hearts, int id, List<Loot> loot, SqlDatabase reader)
        {
            Name = name;
            Hearts = hearts;
            Loot = loot;
            _reader = reader;
            GetLoot(id);
        }

        public async Task GetLoot(int monsterId)
        {
            var monsterLoot = await _reader.GetMonsterLootId(monsterId);
            monsterLoot.ForEach(x =>
            {
                Loot.Add(new Loot(x.Name, x.Quantity));
            });
          
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

        public void LoseHearts()
        {
            Hearts -= 3;
        }

        public void DamageSteve(Steve steve)
        {
            steve.LoseHearts();
        }

    }
}
