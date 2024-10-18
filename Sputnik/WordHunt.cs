using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sputnik
{
    internal class WordHunt : IMiniGame
    {
        public string Name => "Word Hunt";

        //Basically like countdown
        //First with a given set of letters
        //then maybe allow user to pick letters
        //timed
        //allow user to choose just like countdown (or at least the last two chars)

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");

            while (!token.IsCancellationRequested)
            {
              
            }
            await Task.CompletedTask;
        }
    }
}
