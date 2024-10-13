using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sputnik
{
    internal class WordHunt : IMiniGame
    {
        public string Name => "Word Hunt";

        public async Task Play()
        {
            Console.WriteLine($"You are playing {Name}!");
        }
    }
}
