using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;

#if !DEBUG
using System.IO;
#endif

namespace ProjectGamma
{
    class Program
    {
        static void Main(string[] args)
        {
#if !DEBUG
            using(var stream = new StreamWriter("log.txt"))
#endif
            {
#if !DEBUG
                // Route the console output to a log file
                stream.AutoFlush = true;
                Console.SetOut(stream);
#endif
                var mode = new VideoMode(1280, 720);
                var settings = new ContextSettings(64, 8, 0);
                var style = Styles.Close | Styles.Titlebar;

                using (var game = new Game(mode, "Project Gamma", style, settings))
                {
                    game.Init();
                }
            }
        }
    }
}
