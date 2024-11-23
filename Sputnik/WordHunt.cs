namespace Sputnik
{
    internal class WordHunt : IMiniGame
    {
        public string Name => "Word Hunt";

        private const int minWordLength = 2;

        //Basically like countdown
        //First with a given set of letters
        //then maybe allow user to pick letters
        //timed
        //allow user to choose just like countdown (or at least the last two chars)

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");
            List<string> words = new();
            var letters = LetterGenerator.Generate(new Random());
            Console.WriteLine("Enter as many words as you can from the word:");
            Console.WriteLine(letters);
            var timeoutTask = Task.Delay(30000);
            while(!timeoutTask.IsCompleted) 
            {
                var inputTask = Task.Run(() => ListenForInput(token), token);
                var completedTask = await Task.WhenAny(timeoutTask, inputTask);

                if (completedTask == inputTask)
                {
                    var word = await inputTask;
                    if(word != null)
                    {
                        var isValid = await WordValidator.Validate(word, letters);
                        if (isValid)
                        {
                            words.Add(word);
                            var points = WordHuntScoreCalculator.Calculate(word, minWordLength, letters.Length);
                            Console.WriteLine(points + " points!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid word");
                        }
                    }
                }
                else
                {
                    var position = Console.CursorTop;
                    Console.SetCursorPosition(0, position + 1);
                    Console.WriteLine("Time's up!");
                    break;
                }
                
            }
            var totalScore = WordHuntScoreCalculator.GetFinalScore();
            Console.WriteLine($"Total Score: {totalScore}");
        }


        private static string? ListenForInput(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadLine();
                    return input;
                }
            }
            throw new OperationCanceledException();
        }
    }
}
