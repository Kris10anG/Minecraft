using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Nether
    {
        public List<Monster> NetherMonsters;
        public Random Random;

        public Nether()
        {
            NetherMonsters = new List<Monster>
            {
                new Monster("Blaze", new List<Loot> {new Loot("Blaze rod", 1)})
            };
            Random = new Random();
        }

        public void FindNehterCastle()
        {
            
        }
        public void BlazeSpawner()
        {

        }
        
        public void DropBlazeRod()
        {

        }

        public void NetherHandler()
        {

        }
    }
}
