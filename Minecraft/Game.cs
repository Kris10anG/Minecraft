using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    public class Game
    {
        public async Task Init(string connectionstring)
        {
            var sqlreader = new SqlDatabase(connectionstring);
            var steve = new Steve(10, 64, "pickaxe", "sword", sqlreader);
            var mine = new Mine(steve, sqlreader);
            var nether = new Nether(steve, sqlreader);
            var craftingtable = new Furnace(steve, sqlreader);
            while (true) //til enderdragon er drept!
            {
                Console.WriteLine($"Hva vil du gjøre?\n" +
                                  $"Utforske hula: 1\n" +
                                  $"Steve Sine egenskaper: 2\n" +
                                  $"Smelte iron ore: 3\n" +
                                  $"Mine flint fra gravel: 4\n" +
                                  $"Lage Tenner: 5\n" +
                                  $"Dra til Nether: 6\n" +
                                  $"Dra til End: 7");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        mine.MineMode();
                        break;
                    case "2":
                       await steve.SteveHandler();
                        break;
                    case "3":
                       await steve.Melt();
                        break;
                    case "4":
                        steve.MineGravelToFlint();
                        break;
                    case "5":
                       await steve.MakeFlint();
                        break;
                    case "6":
                        var enoughObsidian = await steve.CheckForEnoughObsidian();
                        var flint = await steve.CheckForFlintAndSteel();
                        if (enoughObsidian && flint)
                        {
                            nether.NetherHandler();
                        }
                        else
                        {
                            Console.WriteLine($"Du kan ikke lage portal til nether");
                        }
                        break;
                    case "7":
                        break;
                }
            }
        }
    }
}
