using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class End
    {
        public Steve Steve { get; private set; }
        public SqlDatabase Reader { get; private set; }
        public Enderdragon Enderdragon { get; private set; }


        public End(Steve steve, SqlDatabase reader, Enderdragon enderdragon)
        {
            Steve = steve;
            Reader = reader;
            Enderdragon = enderdragon;
        }

        public void EndHandler()
        {
            Console.WriteLine($"You are now in End!");
            while (true)
            {
            var userOption = Console.ReadLine();
            switch (userOption)
            {
                    case "1":
                        FightEnderdragon();
                        break;
            }
            }
        }

        public async Task FightEnderdragon()
        {
            while (Steve.Hearts >= 0 && Enderdragon.Hearts >= 0)
            {
                Console.WriteLine($"q: attack\n " +
                                  $"w: use item\n" +
                                  $"e: escape\n");
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "q":
                        Steve.AttackEnderdragon(Enderdragon);
                        Steve.TakeDamageFromEnderdragon(Enderdragon);
                        Steve.NoHearts("x");
                        if (Enderdragon.Hearts <= 0 || Steve.Hearts <= 0)
                        {
                            return;
                        }
                        break;
                    case "w":
                        await Steve.UseItem();
                        break;
                    case "e":
                        break;
                }
                {

                }
            }
        }
    }
}
