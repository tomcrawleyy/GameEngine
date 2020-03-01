using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "OpenTK"))
            {
                game.Run(60.0);
            }
        }
    }
}
