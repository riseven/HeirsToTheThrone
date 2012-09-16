using System;

namespace HeirsToTheThrone
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HeirsGame game = new HeirsGame())
            {
                game.Run();
            }
        }
    }
}

