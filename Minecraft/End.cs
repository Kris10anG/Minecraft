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

        public End(Steve steve, SqlDatabase reader)
        {
            Steve = steve;
            Reader = reader;
        }


    }
}
