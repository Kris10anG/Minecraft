using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    public static class Commen //fellesklasse
    {
        public static List<Block> Blocks { get; set; }

      
        public static async Task InitiateList(SqlDatabase reader)
        {
            
            Blocks = await reader.GetBlocks();

        }
    }

}
