#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Diploma2015
{
#if WINDOWS || LINUX
    public static class Start
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}